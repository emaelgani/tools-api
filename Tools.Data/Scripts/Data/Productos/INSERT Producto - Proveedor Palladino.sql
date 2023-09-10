-- Insertar un nuevo proveedor
INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'PALLADINO', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

-- Insertar productos utilizando el IdProveedor obtenido y establecer 'Actualizado' con la fecha de hoy
INSERT INTO `maurodb`.`producto`
(`Nombre`, `Codigo`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Actualizado`)
VALUES
('Lámpara c/imán 12v O 220 c/Led (redonda)', 9, 'PALLADINO', 8, 12480.00, 24960, 29952, @IdProveedor, CURDATE()),
('Tablero de mesa rodante (080 X 045 mts) con 20 ganchos', 39, 'PALLADINO', 1, 23040.00, 46080, 55296, @IdProveedor, CURDATE()),
('Mesa rodante 2 bandejas con ruedas reforzadas', 16, 'PALLADINO', 2, 40080.00, 80160, 96192, @IdProveedor, CURDATE()),
('Camilla de chapa', 25, 'PALLADINO', 1, 23880.00, 47760, 57312, @IdProveedor, CURDATE()),
('Tablero portaherramientas (200 X 080 mts) con acc', 38, 'PALLADINO', 3, 48360.00, 96720, 116064, @IdProveedor, CURDATE()),
('Mesa rodante con cajón 3 estantes', 14, 'PALLADINO', 1, 65160.00, 130320, 156384, @IdProveedor, CURDATE()),
('Tablero portaherramientas (140 X 080 mts) con acc', 37, 'PALLADINO', 1, 37440.00, 74880, 89856, @IdProveedor, CURDATE()),
('Ganchos para tablero x 20 unidades', 42, 'PALLADINO', 3, 3000.00, 6000, 7200, @IdProveedor, CURDATE()),
('Ordenador de pared 30 cajones', 46, 'PALLADINO', 1, 56640.00, 113280, 135936, @IdProveedor, CURDATE());
