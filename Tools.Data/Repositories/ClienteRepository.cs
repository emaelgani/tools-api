using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Cliente;

namespace Tools.Data.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly HerramientasDbContext _context;

        public ClienteRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task<IList<ClienteVentaProductoDTO>> GetClientesVentaByProducto(int IdProducto)
        {
            try
            {
                var idProductoParam = new MySqlParameter("@IdProducto", MySqlDbType.Int32) { Value = IdProducto };

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "CALL GetClienteVentaByProducto(@IdProducto)";
                    command.Parameters.Add(idProductoParam);

                    if (command.Connection?.State != ConnectionState.Open)
                        await command.Connection!.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var clientes = new List<ClienteVentaProductoDTO>();

                        while (await reader.ReadAsync())
                        {
                            var cliente = new ClienteVentaProductoDTO
                            {
                                IdCliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Direccion = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                UltimaVenta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                CantidadVendidos = reader.GetInt32(5),
                            };

                            clientes.Add(cliente);
                        }

                        return clientes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los clientes desde el stored procedure", ex);
            }
        }
    }
}
