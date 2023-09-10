using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Globalization;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Cliente;
using Tools.Shared.Exceptions;

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

        public async Task<IActionResult> CreatePdfSalida()
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                PdfDocument document = new();
                PdfPage page = document.AddPage();

                var clientes = await _clienteRepo.GetAllAsync();

                bool firstTime = true;
                int nroPage = 0;
                int currentPageItem = 0;
                int maxItemsPerPage = 35;
                DateTime fechaActual = DateTime.Now;
                string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");


                foreach (var cliente in clientes)
                {
                    if (cliente.Deuda > 0)
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

                                gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(20, 80), new XPoint(120, 80));

                                // Generate table header
                                gfx.DrawString("CLIENTE", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(40, 120));
                                gfx.DrawString("SALDO", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(140, 120));
                                gfx.DrawString("PAGO", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(240, 120));
                                gfx.DrawString("OBSERVACIÓN", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(440, 120));

                                gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(40, 122), new XPoint(550, 122));

                            }

                            gfx.DrawString(cliente.Nombre.ToUpper(), new XFont("Arial", 7), XBrushes.Black, new XPoint(40, currentYPositionValues));
                            gfx.DrawString(cliente.Deuda.ToString("C", new CultureInfo("es-AR")), new XFont("Arial", 8), XBrushes.Black, new XPoint(140, currentYPositionValues));
                            gfx.DrawString(String.Empty, new XFont("Arial", 7), XBrushes.Black, new XPoint(240, currentYPositionValues));
                            gfx.DrawString(String.Empty, new XFont("Arial", 7), XBrushes.Black, new XPoint(440, currentYPositionValues));
                            gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), new XPoint(40, currentYPositionLine), new XPoint(550, currentYPositionLine));
                        }

                        currentPageItem++;

                    }
                }

                using (MemoryStream memoryStream = new())
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
                throw new Exception("Error al crear pdf de salida.", ex);
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

        public async Task<IList<ClienteVentaProductoDTO>> GetClientByIdProduct(int idProducto)
        {
            try
            {
                return await _clienteRepo.GetClientesVentaByProducto(idProducto);
            }
            catch (Exception)
            {

                throw;
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
