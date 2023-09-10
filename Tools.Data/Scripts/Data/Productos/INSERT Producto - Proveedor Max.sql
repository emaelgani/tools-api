-- Insertar un nuevo proveedor
INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'MAX', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

-- Insertar productos utilizando el IdProveedor obtenido y establecer 'Actualizado' con la fecha de hoy
INSERT INTO `maurodb`.`producto`
(`Nombre`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Actualizado`)
VALUES
('lubrimota', 'Lubrimota', 2, 1200.00, 1800, 2160, @IdProveedor, CURDATE()),
('CepCircOndulado 150mm multieje', 'harden', 1, 3800.00, 5700, 6840, @IdProveedor, CURDATE()),
('Jgo.CincelPunzon 6Pz.HARDEN', 'harden', 1, 7000.00, 10500, 12600, @IdProveedor, CURDATE()),
('Tablero Torx 8pz (10-45) Rucci', 'rucci', 1, 15000.00, 22500, 27000, @IdProveedor, CURDATE()),
('Limpiamanos CLEAN ORANGE x 1 kgs', 'clean orange', 2, 2200.00, 3300, 3960, @IdProveedor, CURDATE());
