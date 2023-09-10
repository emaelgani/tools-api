
CREATE TABLE `compromiso` (
  `IdCompromiso` int NOT NULL AUTO_INCREMENT,
  `IdProveedor` int NOT NULL,
  `Fecha` date NOT NULL,
  `Estado` bit(1) NOT NULL,
  `Monto` decimal(12,2) NOT NULL,
  `PagoDigital` decimal(12,2) DEFAULT NULL,
  `PagoEfectivo` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`IdCompromiso`),
  KEY `IdProveedor` (`IdProveedor`),
  CONSTRAINT `compromiso_ibfk_1` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
