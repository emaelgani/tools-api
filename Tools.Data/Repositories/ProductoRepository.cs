using Microsoft.EntityFrameworkCore;
using System.Data;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs;

namespace Tools.Data.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly HerramientasDbContext _db;

        public ProductoRepository(HerramientasDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Obtiene el producto con el proveedor asociado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Producto> GetProductoWithProveedorAsync(int id)
        {
            try
            {
                return (await _db.Producto
                                .Include(p => p.Proveedor)
                                .SingleOrDefaultAsync(p => p.IdProducto == id))!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting product with proveedor.", ex);
            }

        }

        /// <summary>
        /// Calcula el stock lista
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> CalculateListStockValueAsync()
        {
            try
            {
                return await _db.Producto.SumAsync(p => p.Stock * p.PrecioLista);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try calculated stock value.", ex);
            }
        }

        /// <summary>
        /// Calcula el sotkc financiado
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> CalculateFinanceStockValueAsync()
        {
            try
            {
                return await _db.Producto.SumAsync(p => p.Stock * p.PrecioFinanciado);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try calculated finance stock value.", ex);
            }
        }

        /// <summary>
        /// Calcula el stock al contado
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> CalculateCashStockValueAsync()
        {
            try
            {
                return await _db.Producto.SumAsync(p => p.Stock * p.PrecioContado);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try calculated cash stock value.", ex);
            }
        }

        /// <summary>
        /// Ejecuta stored procedure para obtener el listado de productos.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<Producto2DTO>> GetProductsAsync()
        {
            try
            {
                var productos = new List<Producto2DTO>();

                using (var command = _db.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "CALL GetProductos();";

                    if (command.Connection?.State != ConnectionState.Open)
                        await command.Connection!.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Mapear los datos de la consulta a un DTO personalizado
                            var productoDto = new Producto2DTO
                            {
                                // Mapear los campos del DTO desde las columnas de la consulta
                                IdProducto = reader.GetInt32(0),
                                NombreProveedor = reader.GetString(1),
                                Marca = reader.GetString(2),
                                Codigo = reader.GetString(3),
                                Stock = reader.GetInt32(4),
                                PrecioLista = reader.GetDecimal(5),
                                PrecioContado = reader.GetDecimal(6),
                                PrecioFinanciado = reader.GetDecimal(7),
                                Descripcion = reader.GetString(8),
                                CantidadVentas = reader.GetInt32(9),
                                Nombre = reader.GetString(10),
                                IdProveedor = reader.GetInt32(11),
                                Actualizado = reader.GetDateTime(12)
                            };

                            productos.Add(productoDto);
                        }
                    }
                }

                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al llamar al procedimiento almacenado y obtener DTOs de productos.", ex);
            }
        }
    }
}
