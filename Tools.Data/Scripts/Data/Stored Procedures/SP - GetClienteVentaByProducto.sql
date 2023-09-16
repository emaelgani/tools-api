DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetClienteVentaByProducto`(IN IdProducto INT)
BEGIN
     SELECT c.IdCliente, c.Nombre, c.Direccion,
        c.Telefono, MAX(v.Fecha) AS FechaVentaMasReciente,
        COALESCE(SUM(vp.Cantidad), 0) AS CantidadTotalVendidos
    FROM Cliente c
        LEFT JOIN Venta v ON v.IdCliente = c.IdCliente
        LEFT JOIN VentaProducto vp ON vp.IdVenta = v.IdVenta AND vp.IdProducto = IdProducto
   GROUP BY c.IdCliente, c.Nombre, c.Direccion, c.Telefono;
END$$
DELIMITER ;
