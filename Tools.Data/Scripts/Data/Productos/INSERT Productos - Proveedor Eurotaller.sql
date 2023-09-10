-- Insertar un nuevo proveedor
INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'EUROTALLER', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

-- Insertar productos utilizando el IdProveedor obtenido y establecer 'Actualizado' con la fecha de hoy
INSERT INTO `maurodb`.`producto`
(`Nombre`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Actualizado`)
VALUES
('cargador de baterias', 'Dowen Pagio', 1, 11294.99, 17000, 23800, @IdProveedor, CURDATE()),
('Saca bujia 16', 'Crossmaster', 1, 2447.63, 5000, 7000, @IdProveedor, CURDATE()),
('saca bujia 21', 'Crossmaster', 1, 2447.63, 5000, 7000, @IdProveedor, CURDATE()),
('JUEGO DE DESTORNILLADORES  CHICOS CROSSMASTER', 'Crossmaster', 1, 1647.62, 5000.00, 7000, @IdProveedor, CURDATE()),
('PINZA DE PUNTA 1/2 CAÑA', 'Crossmaster', 1, 2412.84, 5000.00, 7000, @IdProveedor, CURDATE()),
('PINZA DE PUNTA PLANTA', 'Crossmaster', 4, 2532.00, 5000.00, 7000, @IdProveedor, CURDATE()),
('PINZA PICO DE LORO - HERRAMIENTAS', 'Crossmaster', 4, 3166.66, 5000.00, 7000, @IdProveedor, CURDATE()),
('ALICATE - HERRAMIENTAS', 'Crossmaster', 4, 2634.96, 5000.00, 7000, @IdProveedor, CURDATE()),
('llave impacto a bateria 18v 420 NM', 'Dowen Pagio', 3, 81776.19, 110000.00, 154000, @IdProveedor, CURDATE()),
('sierra sable bateria', 'Dowen Pagio', 1, 35000.00, 50000, 70000, @IdProveedor, CURDATE()),
('amoladora angular a batería', 'Dowen Pagio', 2, 77000.00, 115000, 161000, @IdProveedor, CURDATE()),
('inflador a batería', 'Dowen Pagio', 1, 22000.00, 28000, 39200, @IdProveedor, CURDATE()),
('aspiradora a batería', 'Dowen Pagio', 1, 32300.00, 48000, 67200, @IdProveedor, CURDATE()),
('pistola calor', 'Dowen Pagio', 1, 12219.33, 20000, 28000, @IdProveedor, CURDATE()),
('hoja sierra sable', NULL, 2, 1652.06, 3000, 4200, @IdProveedor, CURDATE());
