DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetQuinceProductosMasCompradosPorClientes`(
    IN FechaInicio DATE,
    IN FechaFin DATE
)
BEGIN



    -- Crear la tabla temporal
    CREATE TEMPORARY TABLE productosvendidosaclientes (
        NombreCliente VARCHAR(100),
        NombreProducto VARCHAR(100),
        IdProducto INT,
        NombreProveedor VARCHAR(100),
        CantidadProductos INT
    );


   

    -- Insertar los datos en la tabla temporal
    INSERT INTO productosvendidosaclientes (NombreCliente, NombreProducto, IdProducto, NombreProveedor, CantidadProductos)
    SELECT 
        c.Nombre AS NombreCliente,
        p.Nombre AS NombreProducto,
        p.IdProducto AS IdProducto,
        pr.Nombre AS NombreProveedor,
        SUM(vp.Cantidad) AS CantidadProductos
    FROM
        venta v
    INNER JOIN 
        cliente c ON c.IdCliente = v.IdCliente
    INNER JOIN
        maurodb.VentaProducto vp ON vp.IdVenta = v.IdVenta
    INNER JOIN 
        producto p ON p.IdProducto = vp.IdProducto
    INNER JOIN 
        proveedor pr ON p.IdProveedor = pr.IdProveedor
    WHERE
        v.Fecha >= FechaInicio AND v.Fecha <= FechaFin
    GROUP BY
        c.Nombre,
        p.Nombre,
        p.IdProducto,
        pr.Nombre;

    CREATE TEMPORARY TABLE productosmasvendidos (
        IdProducto INT,
        SumaCantidadProductos INT
    );

    INSERT INTO productosmasvendidos (IdProducto, SumaCantidadProductos)
    SELECT 
        t.IdProducto,
        SUM(t.CantidadProductos) AS SumaCantidadProductos
    FROM
        productosvendidosaclientes t
    GROUP BY
        t.IdProducto
    ORDER BY
        SumaCantidadProductos DESC
    LIMIT 15;

    SELECT
        pmvc.NombreCliente,
        pmvc.NombreProducto,
        pmvc.CantidadProductos
    FROM productosvendidosaclientes pmvc
    INNER JOIN productosmasvendidos pmv ON pmvc.IdProducto = pmv.IdProducto;
    DROP TEMPORARY TABLE productosmasvendidos;
    DROP TEMPORARY TABLE productosvendidosaclientes;
END$$
DELIMITER ;
