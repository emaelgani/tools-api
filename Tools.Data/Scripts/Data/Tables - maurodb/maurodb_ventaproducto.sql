
CREATE TABLE `ventaproducto` (
  `IdVentaProducto` int NOT NULL AUTO_INCREMENT,
  `IdVenta` int NOT NULL,
  `IdProducto` int NOT NULL,
  `IdTipoPrecio` int NOT NULL,
  `Cantidad` int NOT NULL,
  `EsPrecioEspecial` bit(1) NOT NULL,
  `PrecioUnidad` decimal(12,2) NOT NULL,
  `Total` decimal(12,2) NOT NULL,
  PRIMARY KEY (`IdVentaProducto`),
  KEY `IdVenta` (`IdVenta`),
  KEY `IdProducto` (`IdProducto`),
  KEY `IdTipoPrecio` (`IdTipoPrecio`),
  CONSTRAINT `ventaproducto_ibfk_1` FOREIGN KEY (`IdVenta`) REFERENCES `venta` (`IdVenta`) ON DELETE CASCADE,
  CONSTRAINT `ventaproducto_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`) ON DELETE CASCADE,
  CONSTRAINT `ventaproducto_ibfk_3` FOREIGN KEY (`IdTipoPrecio`) REFERENCES `tipoprecio` (`IdTipoPrecio`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
