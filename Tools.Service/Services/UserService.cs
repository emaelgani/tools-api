using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.User;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

namespace Tools.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IMapper mapper, IUserRepository userRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<UserResponseDTO?> CheckToken()
        {
            try
            {
                // Acceder a la identidad del usuario a través del objeto HttpContext.User.
                var username = GetNameToken();
                if (string.IsNullOrEmpty(username))
                {
                    // El usuario no está autenticado, devolvemos null.
                    throw new UnauthorizedAccessException("User not authenticated.");
                }
                // Aquí puedes realizar cualquier verificación adicional según tus necesidades.

                // buscar al usuario en la base de datos usando el nombre de usuario.
                var user =  _userRepo.FindByCondition(u => u.Username == username).FirstOrDefault();
                if (user is null)
                {
                    // El usuario no existe en la base de datos, devolvemos null.
                    throw new NotFoundException("User not found.");
                }
                // Puedes realizar cualquier verificación adicional según tus necesidades.
                // Por ejemplo, puedes comprobar algún estado específico del usuario.

                // Si todo está bien, puedes crear el token y devolver la respuesta.
                string token = CreateToken(user);

                var response = new UserResponseDTO
                {
                    Username = user.Username,
                    Token = token,
                };

                return Task.FromResult(response)!;
            }
            catch (NotFoundException)
            {
                return Task.FromResult<UserResponseDTO>(null!)!;
            }
            catch (UnauthorizedAccessException)
            {
                return Task.FromResult<UserResponseDTO>(null!)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el token.", ex);
            }
        }

        public Task<UserResponseDTO> Login(UserDTO userDto)
        {
            try
            {
                var existingUser = _userRepo.FindByCondition(u => u.Username == userDto.Username).FirstOrDefault();

                if (existingUser is null)
                {
                    // El usuario no existe en la base de datos.
                    throw new NotFoundException("User not found.");
                }
                // Verificar si la contraseña coincide utilizando BCrypt.
                if (!BCrypt.Net.BCrypt.Verify(userDto.Password, existingUser.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Wrong password.");

                }

                // Generar el token JWT.
                string token = CreateToken(existingUser);

                var response = new UserResponseDTO
                {
                    Token = token,
                    Username = existingUser.Username
                };

                return Task.FromResult(response);
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while login a user", ex);
            }
        }

        public async Task Register(UserDTO userDto)
        {
            try
            {
                // Verificar si el usuario ya existe en la base de datos
                var existingUser = _userRepo.FindByCondition(u => u.Username == userDto.Username).FirstOrDefault();
                if (existingUser is not null)
                {
                    // El usuario ya existe, devolver una respuesta indicando que el usuario ya está registrado.
                    throw new Exception("User exist.");
                }

                // El usuario no existe en la base de datos, proceder con el registro.
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                // Mapear el DTO UserDTO al modelo User usando AutoMapper
                var newUser = _mapper.Map<User>(userDto);
                newUser.PasswordHash = passwordHash;

                // Agregar el nuevo usuario a la base de datos
                _userRepo.Add(newUser);
                await _userRepo.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while register a user", ex);

            }
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        
        private string GetNameToken()
        {
            // Obtener el token JWT de la cabecera de autorización
            var authorizationHeader = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedAccessException("Token not provided or invalid.");
            }

            // Extraer el token JWT sin "Bearer "
            var token = authorizationHeader.Substring("Bearer ".Length);

            // Decodificar el token JWT para obtener la información de identidad
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Acceder al nombre de usuario desde el token JWT
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("User not authenticated.");
            }

            return username;
        }
    }

}
