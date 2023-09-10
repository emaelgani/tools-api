CREATE TABLE `producto` (
  `IdProducto` int NOT NULL AUTO_INCREMENT,
  `Marca` varchar(150) DEFAULT NULL,
  `Codigo` varchar(150) DEFAULT NULL,
  `Descripcion` varchar(256) DEFAULT NULL,
  `PrecioContado` decimal(15,2) DEFAULT NULL,
  `PrecioFinanciado` decimal(15,2) DEFAULT NULL,
  `PrecioLista` decimal(15,2) DEFAULT NULL,
  `Stock` int NOT NULL,
  `IdProveedor` int NOT NULL,
  `Nombre` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `Actualizado` date DEFAULT NULL,
  PRIMARY KEY (`IdProducto`),
  KEY `IdProveedor` (`IdProveedor`),
  CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
