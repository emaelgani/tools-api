
CREATE TABLE `pedido` (
  `IdPedido` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int NOT NULL,
  `Fecha` date NOT NULL,
  `Estado` bit(1) NOT NULL,
  `Descripcion` varchar(300) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`IdPedido`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `pedido_ibfk_1` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
