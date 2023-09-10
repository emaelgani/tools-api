
using Microsoft.AspNetCore.Mvc;
using Tools.Api.EndPoint.EndPoints;
using Tools.Service.Interfaces;

namespace Tools.Api.EndPoint
{
    public static class EntitiesEndPoints
    {
        public static void MapEntitiesEndPoints(this WebApplication app)
        {
            #region User
            //POST
            app.MapPost("/api/Auth/register", UserEndPoints.Register);
            app.MapPost("/api/Auth/login", UserEndPoints.Login);
            //GET
            app.MapGet("/api/Auth/check-token", UserEndPoints.CheckToken);
            #endregion

            #region Cliente
            // GET 
            app.MapGet("/api/Cliente", ClienteEndPoints.GetAllClientesAsync);
            app.MapGet("/api/Cliente/TotalDeuda", ClienteEndPoints.GetTotalDebt);
            app.MapGet("/api/Cliente/CuatroConMasDeuda", ClienteEndPoints.GetFourMoreDebt);
            app.MapGet("/api/Cliente/ClientesCompraronProducto/{idProducto}", ClienteEndPoints.GetClientesVentaByProducto);
            app.MapGet("/api/Finanzas/DownloadGuiaSalida", ClienteEndPoints.CreatePdfSalida);
            //POST
            app.MapPost("/api/Cliente", ClienteEndPoints.Add);
            //PUT
            app.MapPut("/api/Cliente", ClienteEndPoints.Update);
            #endregion

            #region Proveedor
            //GET
            app.MapGet("/api/Proveedor", ProveedorEndPoints.GetAllProveedoresAsync);
            app.MapGet("/api/Proveedor/DownloadPrecioLista", ProveedorEndPoints.CreateListaPreciosPdf);
            //POST
            app.MapPost("/api/Proveedor", ProveedorEndPoints.Add);
            app.MapPost("/AumentarPorcentaje", ProveedorEndPoints.AumentarPorcentajeProveedor);
            //PUT
            app.MapPut("/api/Proveedor", ProveedorEndPoints.Update);
            #endregion

            #region Producto
            //POST
            app.MapPost("/api/Producto", ProductoEndPoints.Add);

            //GET
            app.MapGet("/api/Producto", ProductoEndPoints.GetProductos);
            app.MapGet("/api/Producto/{id}", ProductoEndPoints.GetProducto);
            app.MapGet("/api/Producto/ValorTotalStock", ProductoEndPoints.GetValorStockLista);
            app.MapGet("/api/Producto/ValorTotalStockFinanciado", ProductoEndPoints.GetValorStockFinanciado);
            app.MapGet("/api/Producto/ValorTotalStockContado", ProductoEndPoints.GetValorStockContado);
            app.MapGet("/api/Producto/DownloadListaPrecios", ProductoEndPoints.CreateListaPreciosPdf);
           
            //PUT
            app.MapPut("/api/Producto", ProductoEndPoints.Update);

            //DELETE
            app.MapDelete("/api/Producto/{id}", ProductoEndPoints.Delete);
            #endregion

            #region Pedido
            //GET
            app.MapGet("/api/Pedido", PedidoEndPoints.GetAllPedidosAsync);
            //POST
            app.MapPost("/api/Pedido", PedidoEndPoints.Add);
            app.MapPost("/api/Pedido/{id}/changestatus", PedidoEndPoints.ChangeStatus);
            //DELETE
            app.MapDelete("/api/Pedido/{id}", PedidoEndPoints.Delete);
            #endregion

            #region Pago
            //GET
            app.MapGet("/api/Pago", PagoEndPoints.GetAllPagosAsync);
            app.MapGet("/PagosPorMes", PagoEndPoints.GetPagosPorMes);
            app.MapGet("/PagosYVentasPorMes", PagoEndPoints.GetPagosYVentasPorMes);

            //POST
            app.MapPost("/api/Pago", PagoEndPoints.Add);
            app.MapGet("/api/Pago/LiquidezDigital", PagoEndPoints.GetLiquidezDigital);
            app.MapGet("/api/Pago/LiquidezEfectivo", PagoEndPoints.GetLiquidezEfectivo);
            #endregion

            #region Venta
            //GET
            app.MapGet("/api/Venta", VentaEndPoints.GetVentas);
            app.MapGet("/CobranzasYVentas", VentaEndPoints.GetCobranzaYVenta);
            app.MapGet("/VentasPorMes", VentaEndPoints.GetVentasPorMes);
            app.MapGet("/api/Ventas/{idVenta}/productos", VentaEndPoints.GetVentaProductos);
            app.MapGet("/VentasPorMesByIdProducto", VentaEndPoints.GetVentasPorMesByIdProducto);
            app.MapGet("/QuinceProductosMasVendidosPorCliente", VentaEndPoints.GetQuinceProductosMasCompradosPorClientes);
            //POST
            app.MapPost("/api/Venta", VentaEndPoints.Register);
            #endregion

            #region Compromiso
            // GET 
            app.MapGet("/api/Compromiso", CompromisoEndPoints.GetAllAsync);
            app.MapGet("/api/Compromiso/{idCompromiso}", CompromisoEndPoints.GetByIdCompromiso);
            app.MapGet("/api/Compromiso/MontoTotalCompromisosNoPagados", CompromisoEndPoints.GetMontoCompromisosNoPagados);
            app.MapGet("/api/Compromiso/MontoTotalEfectivo", CompromisoEndPoints.GetTotalEfectivo);
            app.MapGet("/api/Compromiso/MontoTotalDigital", CompromisoEndPoints.GetTotalDigital);
            app.MapGet("/api/Compromiso/CompromisosNoSaldadosHoy", CompromisoEndPoints.GetCompromisosNoSaldadosDelDia);
            app.MapGet("/Gastos", CompromisoEndPoints.GetGastos);
            //PUT
            app.MapPut("/api/Compromiso", CompromisoEndPoints.Update);
            //POST
            app.MapPost("/api/Compromiso", CompromisoEndPoints.Add);
            //DELETE
            app.MapDelete("/api/Compromiso/{idCompromiso}", CompromisoEndPoints.Delete);
            #endregion
        }
    }
}
