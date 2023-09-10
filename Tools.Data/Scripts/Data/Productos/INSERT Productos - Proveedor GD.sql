-- Insertar un proveedor
INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'GD', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

-- Insertar productos utilizando el IdProveedor obtenido
INSERT INTO `maurodb`.`producto`
(`Nombre`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Descripcion`, `Actualizado`)
VALUES
('Juego tubos 3/4 gd', 'GD', 1, 90000.00, 180000, 198000, @IdProveedor, 'juego de tubos 19-50 con crique y mango de fuerza', CURDATE()),
('Extractor extremo campanita gd', 'GD', 2, 6000.00, 12000, 13200, @IdProveedor, 'extractor de extremos', CURDATE()),
('Caja tubos torx GD', 'GD', 3, 50000, 100000, 110000, @IdProveedor, 'Juego de puntas Torx', CURDATE()),
('Extractor 3 patas 3" GD', 'GD', 2, 18000, 36000, 39600, @IdProveedor, 'Extractor de tipo pata gallo con posibilidad de 3 y 2 patas', CURDATE()),
('Extractor 3 patas 4" GD', 'GD', 2, 24000, 48000, 52800, @IdProveedor, 'Extractor de tipo pata gallo con posibilidad de 3 y 2 patas', CURDATE()),
('Caja tubos Multiestria GD', 'GD', 4, 50000.00, 100000, 110000, @IdProveedor, 'Juego de puntas multiestria', CURDATE());
