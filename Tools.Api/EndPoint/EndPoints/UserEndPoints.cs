using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Tools.Service.DTOs.User;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class UserEndPoints
    {
        public static async Task Register(HttpContext context, IUserService userSrv, UserDTO userDto)
        {
            try
            {
                if (userDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await userSrv.Register(userDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");

            }
        }
        public static async Task<UserResponseDTO?> Login(HttpContext context, IUserService userSrv, UserDTO userDto)
        {
            try
            {
                if (userDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return null;
                }
                return await userSrv.Login(userDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }

        }
        public static async Task<UserResponseDTO?> CheckToken(HttpContext context, IUserService userSrv)
        {
            try
            {
                return await userSrv.CheckToken();
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(ex.Message);
                return null;
            }

        }
        

    }
}
