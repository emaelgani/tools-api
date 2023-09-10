DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetProductos`()
BEGIN
SELECT  p.IdProducto, pro.Nombre As Proveedor, p.Marca, p.Codigo, 
		p.Stock, p.PrecioLista, p.PrecioContado, p.PrecioFinanciado,
		p.Descripcion, COALESCE(SUM(vp.Cantidad), 0) as CantidadVendido, p.Nombre, p.IdProveedor, p.Actualizado
	FROM Producto p 
	LEFT JOIN VentaProducto vp ON p.IdProducto = vp.IdProducto
	INNER JOIN Proveedor pro ON p.IdProveedor = pro.IdProveedor
	GROUP BY  p.IdProducto, pro.Nombre , p.Marca, p.Codigo, 
		p.Stock, p.PrecioLista, p.PrecioContado, p.PrecioFinanciado,
		p.Descripcion, p.Nombre, p.IdProveedor, p.Actualizado;
END$$
DELIMITER ;
