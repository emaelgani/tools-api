-- Insertar un nuevo proveedor
INSERT INTO `maurodb`.`proveedor`
(`IdProveedor`, `Nombre`, `Telefono`, `Descripcion`, `SumaGastoMensual`)
VALUES (NULL, 'MATIAS H', NULL, NULL, 0);

-- Obtener el IdProveedor del proveedor recién insertado
SET @IdProveedor = LAST_INSERT_ID();

-- Insertar productos utilizando el IdProveedor obtenido
INSERT INTO `maurodb`.`producto`
(`Nombre`, `Marca`, `Stock`, `PrecioLista`, `PrecioContado`, `PrecioFinanciado`, `IdProveedor`, `Actualizado`)
VALUES
('FRANCESA 8" TOPTUL', 'TOPTUL', 1, 3000.00, 6000, 7200, @IdProveedor, CURDATE()),
('Flexible para taladro TOPTUL', 'TOPTUL', 2, 2000.00, 4000, 4800, @IdProveedor, CURDATE()),
('Saca Filtro De Aceite Fiat Siena Palio Fire Chico S4', 'NAC', 1, 2210.00, 4420, 5304, @IdProveedor, CURDATE()),
('Saca Filtro Rápido Hamilton 73-85mm', 'NAC', 1, 1905.00, 3810, 4572, @IdProveedor, CURDATE()),
('Puesta Punto Golf Vento Passat Audi Motor 2.0 Tsi Tfsi R4', 'RUHLLMAN', 1, 14151.00, 28302, 33962.4, @IdProveedor, CURDATE()),
('Vacuometro Profesional . Incluye 3 Adaptadores Hamilton', 'HAMILTON', 1, 8129.00, 16258, 19509.6, @IdProveedor, CURDATE()),
('Bandeja imantada HAMILTON', 'HAMILTON', 1, 2873.85, 5747.7, 6897.24, @IdProveedor, CURDATE()),
('Espejo con led HAMILTON', 'HAMILTON', 2, 1938.90, 3877.8, 4653.36, @IdProveedor, CURDATE()),
('Corta remache cadena moto HAMILTON', 'HAMILTON', 1, 2264.35, 4528.7, 5434.44, @IdProveedor, CURDATE()),
('Juego cepillos hamilton HAMILTON', 'HAMILTON', 1, 2059.00, 4118, 4941.6, @IdProveedor, CURDATE()),
('Sondas top tul TOPTUL', 'TOPTUL', 2, 1560.00, 3120, 3744, @IdProveedor, CURDATE()),
('Goniometro Torquimetro De Grados Encastre 1/2 Profesional H1', 'HAMILTON', 2, 3070.00, 6140, 7368, @IdProveedor, CURDATE()),
('Torquimetro Aguja 0 A 20 Kg Encastre 1/2 RUHLMANN', 'RUHLLMAN', 1, 4833.00, 9666, 11599.2, @IdProveedor, CURDATE()),
('Extractor Saca Precap 45', 'NAC', 4, 3686.00, 7372, 8846.4, @IdProveedor, CURDATE()),
('Extractor Saca Precap 41', 'NAC', 1, 3276.00, 6552, 7862.4, @IdProveedor, CURDATE()),
('Extractor Saca Precap 38', 'NAC', 1, 3276.00, 6552, 7862.4, @IdProveedor, CURDATE()),
('Extractor Saca Precap 36', 'NAC', 2, 3276.00, 6552, 7862.4, @IdProveedor, CURDATE()),
('Saca Filtro Aceite Fiat Toro E Tork 1.6 1.8 Jeep P2 (32mm)', 'NAC', 1, 1410.00, 2820, 3384, @IdProveedor, CURDATE()),
('Saca Filtro De Aceite Vw Suran Fox Gol Trend Extra Largo S4', 'NAC', 3, 2655.00, 5310, 6372, @IdProveedor, CURDATE()),
('Saca Filtro Aceite Renault Logan Duster Master 77mm S4 PZ', 'NAC', 2, 2087.00, 4174, 5008.8, @IdProveedor, CURDATE()),
('Saca Filtro De Aceite Sprinter Mercedes Benz 74mm S4', 'NAC', 2, 2087.00, 4174, 5008.8, @IdProveedor, CURDATE()),
('Saca Filtro Aceite Toyota Corolla 64,5mm Original', 'EUROTECH', 2, 2415.00, 4830, 5796, @IdProveedor, CURDATE()),
('Saca Filtro De Aceite Fleje Suncho Acero Universal Rucci R3 (98MM-130MM)', 'NAC', 2, 1240.00, 2480, 2976, @IdProveedor, CURDATE()),
('Saca Filtro De Aceite Fleje Suncho Acero Universal Rucci R3 (72MM-98MM)', 'NAC', 1, 1240.00, 2480, 2976, @IdProveedor, CURDATE()),
('Pistolas para Sopletear Lavar con Tacho pulverizadora Neumatica KLD', 'KLD', 1, 3429.00, 6858, 8229.6, @IdProveedor, CURDATE()),
('Saca Filtro Aceite Fiat Toro E Tork 1.6 1.8 Jeep P2 (36mm)', 'NAC', 1, 1410.00, 2820, 3384, @IdProveedor, CURDATE()),
('Saca Filtro Aceite Fiat Toro E Tork 1.6 1.8 Jeep P2 (32mm)', 'NAC', 1, 1410.00, 2820, 3384, @IdProveedor, CURDATE()),
('Automotor Saca Filtros 54 A 116 mm 3 Patas Chatas Hamilton', 'HAMILTON', 3, 4114.00, 8228, 9873.6, @IdProveedor, CURDATE()),
('bomba purgadora de freno HAMILTON', 'HAMILTON', 2, 9025.00, 18050, 21660, @IdProveedor, CURDATE()),
('extractor De Masas Campanas Peugeot Partner 206 306 405', 'CANDAMIO', 1, 19664.00, 39328, 47193.6, @IdProveedor, CURDATE()),
('juego de llaves combinadas de 8 a 19mm con Estuche Black Jack', 'BLACK JACK', 1, 5650.80, 11301.6, 13561.92, @IdProveedor, CURDATE()),
('HAMILTON PERFORMANCE 152', 'HAMILTON', 1, 78900.00, 157800, 189360, @IdProveedor, CURDATE()),
('BANDEJA BAHCO', 'BAHCO', 2, 15563.50, 31127, 37352.4, @IdProveedor, CURDATE()),
('PP renko k4m', 'NAC', 2, 5060.48, 10120.95, 12145.14, @IdProveedor, CURDATE()),
('PP VW AMAROK. BORA, GOLF GTI VENTO 1,9 TDI Y 2,0 TDI', 'NAC', 2, 5301.45, 10602.9, 12723.48, @IdProveedor, CURDATE()),
('PP FORD 1,6 16V SIGMA KINETIC', 'NAC', 1, 4581.50, 9163, 10995.6, @IdProveedor, CURDATE()),
('PP FIAT MOTOR EVO 1,4 8V 2012 EN ADELANTE', 'NAC', 1, 5060.48, 10120.95, 12145.14, @IdProveedor, CURDATE()),
('PP VW GOL FOX VOYAGE SURAN 1,6 Y 1,4 NAFTA', 'NAC', 2, 5060.48, 10120.95, 12145.14, @IdProveedor, CURDATE()),
('PP FORD RANGER MOTOR PUMA 2,2 Y 3,2', 'NAC', 1, 3735.11, 7470.225, 8964.27, @IdProveedor, CURDATE()),
('Kit extractor tapones karter', 'HAMILTON', 1, 17421, 34842, 41810.4, @IdProveedor, CURDATE()),
('PINZA ABRAZADERAS', 'HAMILTON', 2, 13687, 27374, 32848.8, @IdProveedor, CURDATE()),
('Saca filtro correa bremen', 'BREMEN', 2, 6101, 12202, 14642.4, @IdProveedor, CURDATE()),
('Prensa espirales', 'HAMILTON', 2, 10863, 21726, 26071.2, @IdProveedor, CURDATE()),
('Llaves 1/4 a a 1 11/4', 'HAMILTON', 1, 56250, 112500, 135000, @IdProveedor, CURDATE());
