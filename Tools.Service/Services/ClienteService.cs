using AutoMapper;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.Cliente;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

namespace Tools.Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepo;

        public ClienteService(IMapper mapper, IClienteRepository clienteRepo)
        {
            _mapper = mapper;
            _clienteRepo = clienteRepo;
        }

        public async Task Add(ClienteDTO clienteDto)
        {
            try
            {
                _clienteRepo.Add(_mapper.Map<Cliente>(clienteDto));
                await _clienteRepo.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a client.", ex);
            }
        }

        public async Task<IList<ClienteDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<IList<ClienteDTO>>(await _clienteRepo.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting clients.", ex);
            }
        }

        public async Task<IList<ClienteDTO>> GetFourMoreDebt()
        {
            try
            {

                var clients = await _clienteRepo.GetAllAsync();
                var clientsFilter = clients.OrderByDescending(c => c.Deuda).Take(4).Select(c => _mapper.Map<ClienteDTO>(c));
                return clientsFilter.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while while calculating the four with more debt.", ex);
            }
        }

        public async Task<decimal> GetTotalDebt()
        {
            try
            {
                var clients = await _clienteRepo.GetAllAsync();
                decimal totalDebt = clients.Sum(client => client.Deuda);
                return totalDebt;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while while calculating the total debt.", ex);
            }
        }


        public async Task Update(ClienteDTO clienteDto)
        {
            try
            {
                var existingCliente = await _clienteRepo.FindByIdAsync(clienteDto.IdCliente!);

                if (existingCliente is null)
                {
                    throw new NotFoundException("Client not found.");
                }

                _mapper.Map(clienteDto, existingCliente);

                _clienteRepo.Update(existingCliente);
                await _clienteRepo.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the client.", ex);
            }
        }

    }
}
