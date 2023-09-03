using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Globalization;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.Producto;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs;

namespace Tools.Service.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IMapper _mapper;
        private readonly IProductoRepository _productoRepo;
        private readonly IProveedorRepository _proveedorRepo;

        public ProductoService(IMapper mapper, IProductoRepository productoRepo, IProveedorRepository proveedorRepo)
        {
            _mapper = mapper;
            _productoRepo = productoRepo;
            _proveedorRepo = proveedorRepo;
        }
        public async Task Add(ProductoDTO productoDto)
        {
            try
            {
                productoDto.Actualizado = DateTime.Now;
                var producto = _mapper.Map<Producto>(productoDto);

                var proveedor = await _proveedorRepo.FindByIdAsync(productoDto.IdProveedor);

                if (proveedor is null)
                {
                    throw new NotFoundException("Proveedor does not exits.");
                }

                _productoRepo.Add(producto);

                await _productoRepo.CommitAsync();

            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a product.", ex);
            }
        }

        public async Task<IActionResult> CreateListaPreciosPdf()
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                PdfDocument document = new();
                PdfPage page = document.AddPage();

                XFont font = new("Arial", 9);
                XFont boldFont = new("Arial", 9, XFontStyle.Bold);

                var productos = await _productoRepo.GetAllAsync();

                int currentPageItem = 0;
                int maxItemsPerPage = 40;

                foreach (var producto in productos)
                {
                    if (currentPageItem >= maxItemsPerPage)
                    {
                        page = document.AddPage();
                        currentPageItem = 0;
                    }

                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        int currentYPositionValues = 30 + (currentPageItem * 20);
                        int currentYPositionLine = 32 + (currentPageItem * 20);

                        gfx.DrawString("ID: " + producto.IdProducto.ToString() + ". " + producto.Nombre!, font, XBrushes.Black, new XPoint(1, currentYPositionValues));
                        gfx.DrawString(producto.PrecioFinanciado.ToString("C", new CultureInfo("es-AR")), font, XBrushes.Black, new XPoint(500, currentYPositionValues));
                        gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(0, currentYPositionLine), new XPoint(650, currentYPositionLine));
                    }

                    currentPageItem++;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    document.Save(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    return new FileContentResult(fileBytes, "application/pdf")
                    {
                        FileDownloadName = "salida.pdf"
                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos", ex);
            }
        }

        public async Task DeleteProducto(int id)
        {
            try
            {
                var dbProducto = await _productoRepo.FindByIdAsync(id);

                if (dbProducto is null)
                {
                    throw new NotFoundException("Producto not found.");
                }
                _productoRepo.Remove(dbProducto);

                await _productoRepo.CommitAsync();
               
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try deleting a product", ex);
            }
        }

        //Ejecuta un stored procedure llamado GetProductos.
        public async Task<IList<Producto2DTO>> GetAllAsync()
        {
            try
            {
                return await _productoRepo.GetProductsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting list of products", ex);
            }
        }

        public async Task<Producto2DTO> GetProducto(int id)
        {
            try
            {
                var producto = await _productoRepo.GetProductoWithProveedorAsync(id);

                if (producto is null)
                {
                    throw new NotFoundException("Producto not found.");
                }

                var productoDTO = _mapper.Map<Producto2DTO>(producto);

                return productoDTO;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting a product.", ex);
            }
        }

        public async Task<decimal> GetValorStockContado()
        {
            try
            {
                return await _productoRepo.CalculateCashStockValueAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting a stock finance value.", ex);
            }
        }

        public async Task<decimal> GetValorStockFinanciado()
        {
            try
            {
                return await _productoRepo.CalculateFinanceStockValueAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting a stock finance value.", ex);
            }
        }

        public async Task<decimal> GetValorStockLista()
        {
            try
            {
                return await _productoRepo.CalculateListStockValueAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting a stock product value.", ex);
            }
        }

        public async Task Update(ProductoDTO productoDto)
        {
            try
            {
                var dbProducto = await _productoRepo.FindByIdAsync(productoDto.IdProducto);

                if (dbProducto is null)
                {
                    throw new NotFoundException("Producto not found.");
                }
                if (dbProducto.PrecioLista != productoDto.PrecioLista ||
                    dbProducto.PrecioContado != productoDto.PrecioContado ||
                    dbProducto.PrecioFinanciado != productoDto.PrecioFinanciado)
                {
                    dbProducto.Actualizado = DateTime.Now;
                }

                dbProducto.Marca = productoDto.Marca;
                dbProducto.Nombre = productoDto.Nombre;
                dbProducto.Codigo = productoDto.Codigo;
                dbProducto.Descripcion = productoDto.Descripcion;
                dbProducto.PrecioContado = productoDto.PrecioContado;
                dbProducto.PrecioFinanciado = productoDto.PrecioFinanciado;
                dbProducto.PrecioLista = productoDto.PrecioLista;
                dbProducto.Stock = productoDto.Stock;

                var proveedor = await _proveedorRepo.FindByIdAsync(productoDto.IdProveedor);
                if (proveedor is  null)
                {
                    throw new NotFoundException($"Proveedor with id {productoDto.IdProveedor} does not exists.");
                }
                dbProducto.IdProveedor = productoDto.IdProveedor;

                await _productoRepo.CommitAsync();

            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto", ex);
            }
        }
    }
}
