
CREATE TABLE `venta` (
  `IdVenta` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int NOT NULL,
  `Fecha` date NOT NULL,
  `TotalVenta` decimal(12,2) NOT NULL,
  PRIMARY KEY (`IdVenta`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `venta_ibfk_1` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
