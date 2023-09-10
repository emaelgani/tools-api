
CREATE TABLE `pago` (
  `IdPago` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int NOT NULL,
  `IdMetodoPago` int NOT NULL,
  `Fecha` date NOT NULL,
  `TotalPago` decimal(12,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`IdPago`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdMetodoPago` (`IdMetodoPago`),
  CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`) ON DELETE CASCADE,
  CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`IdMetodoPago`) REFERENCES `metodopago` (`IdMetodoPago`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
