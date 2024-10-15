using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Globalization;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Proveedor;
using Tools.Shared.Exceptions;

namespace Tools.Service.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IMapper _mapper;
        private readonly IProveedorRepository _proveedorRepo;

        public ProveedorService(IMapper mapper, IProveedorRepository proveedorRepo)
        {
            _mapper = mapper;
            _proveedorRepo = proveedorRepo;
        }
        public async Task Add(ProveedorDTO newProveedor)
        {
            try
            {
                var proveedor = _mapper.Map<Proveedor>(newProveedor);
                _proveedorRepo.Add(proveedor);
                await _proveedorRepo.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a proveedor.", ex);
            }
        }

        public async Task AumentarPorcentajeProveedor(ProveedorAumentoDTO proveedorAumento)
        {
            try
            {
                var proveedor = await _proveedorRepo.GetProveedorWithProductsAsync(proveedorAumento.IdProveedor);
                if (proveedor is null)
                {
                    throw new NotFoundException("Proveedor not found.");
                }

                foreach (var producto in proveedor.Productos!)
                {
                    producto.PrecioContado *= (1 + proveedorAumento.Porcentaje / 100);
                    producto.PrecioFinanciado *= (1 + proveedorAumento.Porcentaje / 100);
                    producto.PrecioLista *= (1 + proveedorAumento.Porcentaje / 100);
                    producto.Actualizado = DateTime.Now;
                }

                await _proveedorRepo.CommitAsync();

            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when increasing percentage.", ex);
            }
        }

        public async Task<IActionResult> CreatePdfListaPrecios(int idProveedor)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                PdfDocument document = new();
                PdfPage page = document.AddPage();


                var proveedor = await _proveedorRepo.GetProveedorWithProductsAsync(idProveedor);

                List<Producto> productos = new();

                if (proveedor is not null)
                {
                    productos = proveedor.Productos!.ToList();

                }
                else
                {
                    throw new Exception("Proveedor inexistente");
                }

                bool firstTime = true;
                int nroPage = 0;
                int currentPageItem = 0;
                int maxItemsPerPage = 35;
                DateTime fechaActual = DateTime.Now;
                string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");

                foreach (var producto in productos)
                {
                    if (nroPage > 0)
                    {
                        maxItemsPerPage = 40;
                    }
                    if (currentPageItem >= maxItemsPerPage)
                    {
                        page = document.AddPage();
                        currentPageItem = 0;
                        nroPage++;
                    }

                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        int currentYPositionValues = (nroPage == 0) ? (140 + (currentPageItem * 20)) : (30 + (currentPageItem * 20));
                        int currentYPositionLine = (nroPage == 0) ? (142 + (currentPageItem * 20)) : (32 + (currentPageItem * 20));

                        if (firstTime)
                        {
                            firstTime = false;

                            gfx.DrawString("1122 Herramientas.", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 50));
                            gfx.DrawString($"Fecha: {fechaFormateada}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 70));

                            gfx.DrawString($"Proveedor: {proveedor.Nombre}", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 90));

                            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(20, 100), new XPoint(120, 100));

                            //gereate table header
                            gfx.DrawString("PRODUCTO", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(40, 120));
                            gfx.DrawString("PRECIO LISTA", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(430, 120));

                            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(40, 122), new XPoint(242, 122));
                        }

                        gfx.DrawString(producto.Nombre!.ToUpper(), new XFont("Arial", 7), XBrushes.Black, new XPoint(40, currentYPositionValues));
                        gfx.DrawString(producto.PrecioLista.ToString("C", new CultureInfo("es-AR")), new XFont("Arial", 8), XBrushes.Black, new XPoint(430, currentYPositionValues));
                        gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(40, currentYPositionLine), new XPoint(533, currentYPositionLine));
                    }

                    currentPageItem++;

                }

                using MemoryStream memoryStream = new();
                document.Save(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                return new FileContentResult(fileBytes, "application/pdf")
                {
                    FileDownloadName = "salida.pdf"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el precio lista de los productos del cliente", ex);
            }
        }

        public async Task<IList<ProveedorDTO>> GetAsync()
        {
            try
            {
                return _mapper.Map<IList<ProveedorDTO>>(await _proveedorRepo.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred getting prevedores", ex);
            }
        }

        public async Task<IList<ProveedorWithProductsDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<IList<ProveedorWithProductsDTO>>(await _proveedorRepo.GetAllProveedorWithProductsAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred getting prevedores", ex);
            }
        }

        public async Task Update(ProveedorDTO updateProveedor)
        {
            try
            {
                var proveedor =  _proveedorRepo.FindByCondition(p => p.IdProveedor == updateProveedor.IdProveedor).FirstOrDefault();
                if (proveedor is null)
                {
                    throw new NotFoundException("Proveedor not found.");
                }

                proveedor.Nombre = updateProveedor.Nombre;
                proveedor.Telefono = updateProveedor.Telefono;
                proveedor.Descripcion = updateProveedor.Descripcion;
                proveedor.SumaGastoMensual = updateProveedor.SumaGastoMensual;

                await _proveedorRepo.CommitAsync();

            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a proveedor.", ex);
            }
        }

        public async Task<ProveedorDTO?> GetById(int id)
        {
            try
            {
                var proveedor = _proveedorRepo.FindByCondition(p => p.IdProveedor == id).FirstOrDefault();

                return _mapper.Map<ProveedorDTO>(proveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred getting prevedores", ex);
            }
        }
    }
}
