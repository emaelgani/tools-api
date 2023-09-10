INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'EUROTECH', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

INSERT INTO `maurodb`.`producto`
(`Nombre`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Actualizado`)
VALUES
('Llave corona para freno', 'EUROTECH', 1, 6546.00, 13092, 15710.4, @IdProveedor, CURRENT_DATE()),
('Aceitera CHICA 250cc METALICA con 2 Picos DAVISON', 'DAVISON', 2, 830.00, 1660, 1992, @IdProveedor, CURRENT_DATE()),
('Aceitera GRANDE 500cc METALICA con 2 Picos DAVISON', 'DAVISON', 2, 1285.00, 2570, 3084, @IdProveedor, CURRENT_DATE()),
('Kit Tensor Correa POLY-V (3 P.Cabra+4 Tubos)', 'EUROTECH', 1, 10373.00, 20746, 24895.2, @IdProveedor, CURRENT_DATE()),
('Barra Magnetica Sujeta Herramientas', 'EUROTECH', 4, 2018.00, 4036, 4843.2, @IdProveedor, CURRENT_DATE()),
('Juego Destornilladores Largos 450mm 3 Piezas DAVISON', 'DAVISON', 1, 4554.00, 9108, 10929.6, @IdProveedor, CURRENT_DATE()),
('Juego Destornilladores Largos 450mm 3 Piezas DAVISON', 'DAVISON', 2, 3893.00, 7786, 9343.2, @IdProveedor, CURRENT_DATE()),
('Pinza gomero', 'EUROTECH', 2, 3726.00, 7452, 8942.4, @IdProveedor, CURRENT_DATE()),
('Camilla PLASTICA', 'EUROTECH', 1, 12425.00, 24850, 29820, @IdProveedor, CURRENT_DATE()),
('Prensa Aros 3" (90-175)', 'EUROTECH', 1, 1939.00, 3878, 4653.6, @IdProveedor, CURRENT_DATE()),
('Prensa Aros 4" (57-125)', 'EUROTECH', 2, 2264.00, 4528, 5433.6, @IdProveedor, CURRENT_DATE()),
('Pinza para Acoples Combustible', 'EUROTECH', 3, 3161.00, 6322, 7586.4, @IdProveedor, CURRENT_DATE()),
('Llaves x 3 - Caño - FRENO ( 8-13mm)', 'EUROTECH', 1, 4626.00, 9252, 11102.4, @IdProveedor, CURRENT_DATE()),
('Tubos COLOR 3pc IMPACTO', 'EUROTECH', 1, 10072.00, 20144, 24172.8, @IdProveedor, CURRENT_DATE()),
('Palanca - 24" - de Fuerza 1/2" - EURO', 'EUROTECH', 1, 9774.00, 19548, 23457.6, @IdProveedor, CURRENT_DATE()),
('Caballete 3T (PAR)', 'EUROTECH', 1, 13999.00, 27998, 33597.6, @IdProveedor, CURRENT_DATE()),
('Torx x 10 - Blister GRIS (7 Torx, 1Ph, 1Plano, 1Crique)', 'EUROTECH', 1, 11353.00, 22706, 27247.2, @IdProveedor, CURRENT_DATE()),
('compresometro', 'EUROTECH', 1, 8837, 17674, 21208.8, @IdProveedor, CURRENT_DATE()),
('Extractor rulemanes 25mm', 'EUROTECH', 1, 5464, 10928, 13113.6, @IdProveedor, CURRENT_DATE()),
('Extractor rulemanes 50mm', 'EUROTECH', 1, 7011, 14022, 16826.4, @IdProveedor, CURRENT_DATE()),
('Instalador correas Magnetico', 'EUROTECH', 1, 7374, 14748, 17697.6, @IdProveedor, CURRENT_DATE()),
('Kit montaje oxigeno', 'EUROTECH', 1, 23480, 46960, 56352, @IdProveedor, CURRENT_DATE()),
('lamparas noid', 'EUROTECH', 1, 21256, 42512, 51014.4, @IdProveedor, CURRENT_DATE()),
('llave correa ajustable', 'EUROTECH', 1, 12867, 25734, 30880.8, @IdProveedor, CURRENT_DATE()),
('llave pipa x6', 'EUROTECH', 1, 22469, 44938, 53925.6, @IdProveedor, CURRENT_DATE()),
('Kit de sacafiltros', 'EUROTECH', 1, 37817, 75634, 90760.8, @IdProveedor, CURRENT_DATE()),
('Tubo inyector abierto 27 ventana', 'EUROTECH', 1, 6921, 13842, 16610.4, @IdProveedor, CURRENT_DATE()),
('Sunchoz', 'EUROTECH', 1, 2137, 4274, 5128.8, @IdProveedor, CURRENT_DATE()),
('crique carro', 'EUROTECH', 1, 42000.00, 84000, 100800, @IdProveedor, CURRENT_DATE());
