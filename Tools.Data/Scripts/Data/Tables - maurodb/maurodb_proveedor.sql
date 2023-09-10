
CREATE TABLE `proveedor` (
  `IdProveedor` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Telefono` varchar(25) DEFAULT NULL,
  `Descripcion` varchar(300) DEFAULT NULL,
  `SumaGastoMensual` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`IdProveedor`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
