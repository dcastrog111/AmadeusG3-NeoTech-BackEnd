# Recomendación de viajes Amadeus
Solución BackEnd para proyecto de recomendaciones de destinos de viaje de acuerdo con preferencias y gustos personales de los usuarios. 
Proyecto gestiona registro de usuarios, registro de preferencias, cálculo de destinos, recomendación alojamiento y aerolíneas y módulo de administrador para consulta de historicos y estadisticas de preferencia de usuarios.

## Tabla de Contenido 
* [Tecnologías](#tecnologías)
* [Arquitectura](#arquitectura)
* [Instalación](#instalación)
* [MER](#MER)
* [Script](#Script)
* [Licencia](#licencia)

## Tecnologías
* lenguaje C#
* .NET 9.0
* Base de Datos PostgreSql

## Arquitectura
Proyecto se realizó con una arquitectura de capas, estructurada de la siguiente manera:
1. Modelos
2. Repositorios
3. Servicios
4. Controladores
5. DTOS
6. Mappers

## Instalación
Para instalar y configurar el proyecto realizar lo siguiente:

1. Clonar el repositorio:
   ```bash
   git clone git@github.com:dcastrog111/AmadeusG3-NeoTech-BackEnd.git
   ```
2. instalar las dependecias:
   ```bash
   dotnet restore
   ```
3. Crear archivo appsettings.json en la raiz del proyecto con la siguiente estructura:
   ```bash
   {
    "ConnectionStrings": {
      "DefaultConnection": "Host=[Host];Username=[Username];Password=[Password]
    },
    
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*"
   }
   ```
4. Auctualziar migración de Base de Datos
   ```bash
   dotnet ef database update
   ```
5. Ejecutar
   ```bash
   dotnet watch run
   ```
## MER
Se diseñó el siguiente Modelo de base de datos:

![baseDatos drawio](https://github.com/user-attachments/assets/1981a983-7087-49fe-9d6f-cc14d2d5fc2d)

## Script iniciales de tablas Maestras

1. Tabla Questions
   ```bash
   INSERT INTO "Questions" ("Id", "Question_Text") VALUES
   (1, '¿Qué tipo de entorno prefieres para tus vacaciones?'),
   (2, '¿Qué clima prefieres durante tus vacaciones?'),
   (3, '¿Qué tipo de actividades prefieres hacer durante tus vacaciones?'),
   (4, '¿Qué tipo de alojamiento prefieres?'),
   (5, '¿Cuánto tiempo planeas quedarte de vacaciones?'),
   (6, '¿Cuál es tu rango de edad?');
   ```
2. Tabla Questions_Options
   ```bash
   INSERT INTO "Questions_Options" ("Id","Description","Dato","UrlImg", "QuestionId") VALUES
   (1, 'Playa', 'Las playas no siempre son doradas. Hay playas con arena negra volcánica, rosa coralina y hasta verde olivo. ¡Cada grano de arena cuenta una historia!', './imagen1.jpg', 1),
   (2, 'Montaña', 'Las montañas tienen su propio clima. Al subir una montaña, puedes experimentar diferentes climas en pocos kilómetros. ¡Es como viajar por el mundo sin salir de una misma montaña!', './imagen2.jpg', 1),
   (3, 'Ciudad', 'Muchas ciudades tienen secretos subterráneos. Bajo las calles de muchas ciudades se encuentran redes de túneles, ríos subterráneos y hasta antiguas ruinas. París, por ejemplo, tiene más de 200 kilómetros de túneles subterráneos.', './imagen3.jpg', 1),
   (4, 'Caluroso', 'En muchos lugares con clima cálido se celebran festivales y eventos al aire libre, aprovechando las altas temperaturas.', './Tulum.jpg', 2),
   (5, 'Templado', 'Muchas de las rutas turísticas más famosas del mundo se encuentran en regiones con clima templado, como la Ruta de la Costa Amalfitana en Italia o la Ruta 66 en Estados Unidos.', './Templado.jpg', 2),
   (6, 'Frío', 'En lugares con clima frío, el turismo se concentra principalmente en los meses de invierno, cuando la nieve cubre el paisaje y se pueden practicar deportes como el esquí, el snowboard y el patinaje sobre hielo.', './Frio.jpg', 2),
   (7, 'Deportes y Aventuras', 'Desde las montañas de Nepal hasta los ríos de Costa Rica, existen numerosos destinos que ofrecen experiencias únicas para los amantes de la adrenalina.', './Aventura.jpg', 3),
   (8, 'Cultura y Museos', 'Al visitar los museos, los viajeros pueden imaginar cómo era la vida en la corte real y apreciar la arquitectura y el diseño de una época pasada.', './cultura.jpg', 3),
   (9, 'Relax y Bienestar', 'Al visitar un baño termal, los viajeros pueden conectar con las tradiciones de culturas antiguas y experimentar una forma de relajación que ha sido practicada durante siglos.', './relax.jpg', 3),
   (10, 'Hotel de Lujo', 'Algunos de los hoteles más lujosos del mundo ofrecen experiencias tan exclusivas que incluyen la posibilidad de tener un mayordomo que se encargue de todos tus caprichos, desde preparar un baño relajante hasta hacer reservas en el restaurante más exclusivo.', './hotelujo.jpg', 4),
   (11, 'Hostal o Albergue', 'Muchos de los hostales y albergues más populares del mundo se encuentran ubicados en edificios históricos o con una arquitectura única.', './hostal.jpg', 4),
   (12, 'Airbnb', 'Airbnb o apartamento: Airbnb nació de una necesidad de alojamiento económico durante un evento en San Francisco.', './airbnb.jpg', 4),
   (13, 'Menos de una semana', 'Estudios han demostrado que incluso viajes cortos pueden tener un impacto significativo en la reducción del estrés y la mejora del estado de ánimo.', './findesemana.jpg', 5),
   (14, '1 a 2 semanas', 'Estudios han demostrado que este rango de tiempo permite sumergirte en la cultura local, conocer a fondo un lugar y crear recuerdos duraderos sin sentirte apresurado o abrumado.', './dosemanas.jpg', 5),
   (15, 'Más de dos semanas', 'Viajes prolongados te permiten desconectar completamente de tu rutina diaria y volver a casa sintiéndote renovado y con una nueva perspectiva de la vida.', './calendario.jpg', 5),
   (16, 'Menos de 30 años', 'Viajar en la veintena te ayuda a desarrollar habilidades como la independencia, la adaptabilidad y la tolerancia a la incertidumbre, lo cual es fundamental para tu crecimiento personal.', './veinte.jpg', 6),
   (17, '30 a 50 años', 'A menudo, se busca ir más allá de los destinos turísticos más populares y descubrir lugares menos conocidos, con una mayor conexión con la cultura local.', './treinta.jpg', 6),
   (18, 'Más de 50 años', 'Muchos viajeros mayores se unen a grupos organizados para conocer a personas con intereses similares y compartir experiencias.', './cincuenta.jpg', 6);
   ```
3. Tabla Destinations
   ```bash
   INSERT INTO "Destinations" ("Id","Combination", "First_City_Id", "Second_City_Id") VALUES
   (1,'8490127d889091599f185e8c22b2d521bbae96eb4b2cc9dea3087522ccb175b5', 17, 18),
   (2,'e7b3de6b25fbc62f1852b7f65b2eb428da412e49a411bbf7df4e71da5f291e02', 21, 22),
   (3,'5be6c85cc9d60b7c0231febadb2b9dd9dac580c10863eef728caecbda9595699', 19, 20),
   (4,'dfd0dbda7169123806d8cf3da805ad773c132ba5571c5ead2745e72abe0ee910', 7, 8),
   (5,'f1c45a4266ef9a90c85a67f6841bc12371b4d93458b6ded2033c4a2d6b4b5c26', 37, 38),
   (6,'14ea36ffc835ec22282c8005103c4058bb56b6823b0defa30ef0a309f7f256a0', 31, 32),
   (7,'58f4ede441accc687d3416d8e12960f0259b865972dfc57485d793f4eb6b4c63', 23, 24),
   (8,'592995d293cfceacfe2e507a3a3deb432808440e65fca371fe6456a4563f0c38', 5, 6),
   (9,'54d4a30e9de8567d0fedfaa8699a5a5513490f08975a1e441b5d3c2abbc6ea3e', 27, 28),
   (10,'e1a047397b4b9e13ba5ef01c4eef5b8eb7318594439779f8137eb2764eeea666', 25, 26),
   (11,'941fedba13432003df779bbb9a5597f5c35e48f147607bc4792bf5fccb64f489', 35, 36),
   (12,'93fd5c3c4bc45b746aef841efbd1bc3db4f602930a058f8ccc2745df8f7d64d2', 33, 34),
   (13,'bf270b3528b63367b71eb1b626821d436f77bd0e458e725c764ea9e065964758', 1, 2),
   (14,'2468164110679d500d6ca5920aad1eb10b5ffd0b8eafe96ced6959a2df769743', 13, 14),
   (15,'f93085437b360410c1115ff1f3f0628abc40ae9424159ffce834834df156f655', 11, 12),
   (16,'469055c709d6842987c2d7342ae323a3c0ce1991ec69249fc910407bd012c3c3', 9, 10),
   (17,'b242a9460f4dd83908954ca8316915e104f26a52c05750a11c40c0c426fdee35', 29, 30),
   (18,'357b46b5e9f5edfa200f66089fc99b52dbe319b8b7a62fd7f81f2939bc04031a', 3, 4),
   (19,'3b3ea2353a6c27e9f0e29cd3bb2db37fc44b94ad9ffcf92630c3802d26a2e108', 15, 16);
   ```
4. Tabla Cities
   ```bash
   INSERT INTO "Cities" ("Id", "NombreDestino", "Img", "Pais", "Idioma", "LugarImperdible", "ComidaTipica", "Continente", "NombreHotel", "DescripcionHotel", "ImgHotel", "UrlHotel") VALUES
   (1, 'Playa del Carmen', './PlayaDelCarmen.jpg', 'México', 'Español', 'Chichén-Itzá', 'Salbutes', 'América', 'TRS Coral Hotel', 'Resort de lujo para adultos en Playa del Carmen, México, con atención personalizada.', './hoteles/TRSCoralHotel.jpg', 'http://bit.ly/415jHwE'),
   (2, 'Santorini', './Santorini.jpg', 'Grecia', 'Griego', 'Oia', 'Hummus de Fava', 'Europa', 'Katikies Hotel Santorini', 'Famoso por su servicio, ambiente cálido y sentido del romanticismo.', './hoteles/Katikies_Hotel_Santorin.jpg', 'https://bit.ly/3Zodydr'),
   (3, 'Tulum', './Tulum.jpg', 'México', 'Español', 'Cenote Calavera', 'Ceviche de Pescado', 'América', 'Kore Tulum Retreat and Spa Resort', 'Resort de lujo frente al mar con desayuno a la carta, piscina, jardines y terrazas.', './hoteles/Kore_Tulum_Resort-Tulum-Mexico.jpg', 'https://www.koretulum.com/es'),
   (4, 'Ibiza', './ibiza.jpg', 'España', 'Castellano/Catalán', 'Islote Es Vedrá', 'Sofrit pagès', 'Europa', 'ME Ibiza – The Leading Hotels of the World', 'Resort de lujo frente a la playa de SArgamassa con habitaciones elegantes, piscina privada y jardín propio.', './hoteles/ME_Ibiza_Ibiza-España.jpg', 'https://bit.ly/3B8JfOE'),
   (5, 'Honolulu', './Honolulu.jpg', 'Hawái', 'Ingles/Hawaiano', 'Playa Hapuna', 'Saimin', 'América', 'Outrigger Waikiki Beach Resort', 'Resort tropical en Waikiki Beach con habitaciones inspiradas en la isla, vistas al océano y jacuzzis.', '../../../assets/img/hoteles/outrigger-waikiki-beach-Honolulu-Hawai.jpg', 'https://www.outrigger.com/hawaii/oahu/outrigger-waikiki-beach-resort'),
   (6, 'Malta', './Malta.jpg', 'Malta', 'Ingles/Maltés', 'La Valeta', 'Aljotta', 'Europa', 'Ramla Bay Resort', 'Resort de 3 estrellas en la bahía de Ramla con vistas al Mediterráneo, habitaciones cómodas y piscina.', './hoteles/Ramla_Bay_Resort-Malta.jpg', 'https://www.ramlabayresort.com/'),
   (7, 'Cartagena', './Cartagena.jpg', 'Colombia', 'Español', 'Castillo San Felipe', 'Cazuela de Mariscos', 'América', 'Casa San Agustín', 'Hotel de 5 estrellas en la ciudad amurallada con habitaciones lujosas, spa y vistas al mar Caribe.', './hoteles/Casa_San_Agustín_Cartagena.jpg', 'https://www.hotelcasasanagustin.com/es/home.html'),
   (8, 'Barcelona', './Barcelona.jpg', 'España', 'Castellano/Catalán', 'Sagrada Familia', 'Pa amb tomàquet', 'Europa', 'Hotel Arts Barcelona', 'Hotel de 5 estrellas en la Barceloneta, conocido por su ambiente exclusivo y romántico.', './hoteles/Hotel_Arts_Barcelona.jpg', 'https://bit.ly/497dhPm'),
   (9, 'San Juan', './SanJuan.jpg', 'Puerto Rico', 'Español', 'Viejo San Juan', 'Mofongo', 'América', 'The Ritz-Carlton, San Juan', 'Resort de lujo en Condado con habitaciones elegantes, piscinas, spa y vistas al mar Caribe.', './hoteles/The-Ritz-Carlton_San-Juan-Puerto-Rico.jpg', 'https://bit.ly/3B3bKxk'),
   (10, 'Niza', './niza.jpg', 'Francia', 'Frances', 'Vielle Ville', 'La socca', 'Europa', 'Hotel Negresco', 'Hotel de lujo en la Promenade des Anglais, famoso por su arquitectura, historia y exclusivo ambiente.', './hoteles/hotel-le-negresco-Niza-Francia.jpg', 'https://www.hotel-negresco-nice.com/en'),
   (11, 'Río de Janeiro', './RioDeJaneiro.jpg', 'Brasil', 'Portugués', 'Cristo Redentor', 'Feijoada', 'América', 'Belmond Copacabana Palace', 'Icónico hotel de lujo en la playa de Copacabana, inaugurado en 1923, con habitaciones elegantes.', './hoteles/Belmond_Copacabana_Palace_Rio_janeiro.jpg', 'https://bit.ly/415Nk0J'),
   (12, 'Lisboa', './lisboa.jpg', 'Portugal', 'Portugués', 'Tranvía 28', 'Pasteles de Belem', 'Europa', 'Grande Real Villa Italia Hotel & Spa', 'Hotel de lujo en la Avenida da Liberdade, cerca del centro de la ciudad, con habitaciones elegantes y spa.', './hoteles/Grande_Real_Villa_Italia_Hotel & Spa-lisboa.jpg', 'https://www.granderealvillaitalia.realhotelsgroup.com/'),
   (13, 'Punta Cana', './puntaCana.jpg', 'Republica Dominicana', 'Español', 'Playa Bávaro', 'Bandera Dominicana', 'América', 'Secrets Cap Cana Resort & Spa', 'Resort de lujo en Cap Cana con habitaciones elegantes, vistas al océano, piscinas, spa y restaurantes gourmet.', './hoteles/Secrets_Cap_Cana_PuntaCana-RepublicaDominicana.jpg', 'https://www.capcanaresortspa.com/'),
   (14, 'Algarve', './algarve.jpg', 'Portugal', 'Portugués', 'Tavira', 'Cataplana', 'Europa', 'Martinhal Quinta Family Resort', 'Resort de 4 estrellas diseñado para familias, con habitaciones cómodas, piscinas, parque infantil y actividades.', './hoteles/Martinhal_Quinta_Family_Resort-Algarve-Portugal.jpg', 'https://bit.ly/3Veh8V5'),
   (15, 'Ushuaia', './ushuaia.jpg', 'Argentina', 'Español', 'Montes Martial', 'Cazuela de Centolla', 'América', 'Las Hayas Ushuaia Resort', 'Resort de 5 estrellas con habitaciones elegantes, vistas al Canal Beagle, spa, piscina y restaurantes.', './hoteles/Las_Hayas_Ushuaia_Resort-Ushuaia-Argentina.jpg', 'https://bit.ly/4eOcvs0'),
   (16, 'Reykjavik', './reykjavik.jpg', 'Islandia', 'Islandés', 'Hallgrimskirkja', 'Sopa de Cordero', 'Europa', 'Center Hotels Laugavegur', 'Hotel boutique en el corazón de Reykjavik, cerca de Laugavegur, con habitaciones modernas, restaurante, piscina y sauna.', './hoteles/Center_Hotels_Laugavegur-Reykjavik-Islandia.jpg', 'https://www.centerhotels.com/en/hotel-laugavegur-reykjavik'),
   (17, 'Aspen', './Aspen.jpg', 'EE.UU', 'Ingles', 'Aspen Mountain', 'Parrilla', 'América', 'The Little Nell', 'Hotel de lujo en el corazón de Aspen con habitaciones elegantes, vistas a las montañas, spa y restaurantes.', './hoteles/The_Little_Nell-Aspen-Usa.jpg', 'https://bit.ly/3Ow5KjP'),
   (18, 'Innsbruck', './innsbruck.jpg', 'Austria', 'Alemán', 'Hofkkirche', 'Wiener Schnitzel', 'Europa', 'Hotel Sailer', 'Hotel de 4 estrellas en el centro de Innsbruck, cerca de la estación de tren y el centro histórico, con habitaciones cómodas y spa.', './hoteles/Hotel_Sailer-Innsbruck-Austria.jpg', 'https://www.sailer-inns'),
   (19, 'Bariloche', './Bariloche.jpg', 'Argentina', 'Español', 'Nahuel Huapi', 'Curanto', 'América', 'Catalonia Sur Aparts', 'Resort con apartamentos modernos y vistas al lago Nahuel Huapi, cada uno con cocina y detalles arquitectónicos en piedra y madera.', './hoteles/Catalonia_Sur_Aparts-Bariloche.jpg', 'https://www.cataloniasur.com.ar/'),
   (20, 'Interlaken', './interlaken.jpg', 'Suiza', 'Alemán', 'Höhematte Park', 'Raclette', 'Europa', 'Victoria Jungfrau Grand Hotel & Spa', 'Hotel de 5 estrellas con habitaciones elegantes, spa, restaurantes y vistas al Jungfrau y los Alpes suizos.', './hoteles/Victoria_Jungfrau_Grand_Hotel_Suiza.jpg', 'https://www.victoria-jungfrau.ch/'),
   (21, 'Banff', './banff.jpg', 'Canadá', 'Inglés', 'Upper Hot Springs', 'Poutine', 'América', 'Fairmont Banff Springs', 'Icónico hotel de lujo conocido como "The Castle in the Rockies", con habitaciones elegantes, restaurantes y spa.', './hoteles/fairmont-banff-springs-Banff-Canada.jpg', 'https://www.fairmont.com/banff-springs/'),
   (22, 'Zermatt', './zermatt.jpg', 'Suiza', 'Alemán', 'Ferrocarril de Gornergrat', 'Raclette', 'Europa', 'Hotel Alphubel', 'Hotel en el centro de Zermatt, cerca de la estación de tren Gornergrat, con vistas al Monte Cervino.', './hoteles/Hotel_Alphubel-Zermatt-Suiza.jpg', 'https://alphubel-zermatt.ch/en/'),
   (23, 'Cusco', './cusco.jpg', 'Perú', 'Español', 'Saqsaywaman', 'Chiri Uchu', 'América', 'Palacio del Inka, a Luxury Collection Hotel', 'Hotel de lujo con habitaciones elegantes, spa, restaurantes y vistas a la ciudad y alrededores.', './hoteles/Palacio_del_Inka_Cusco-Peru.jpg', 'https://bit.ly/3ZoC0vr'),
   (24, 'Granada', './Granada.jpg', 'España', 'Español', 'Alhambra', 'Pionono', 'Europa', 'Gran Hotel Luna de Granada', 'Hotel de lujo en el barrio de Ronda, cerca del centro histórico de Granada, con habitaciones elegantes, piscina cubierta y al aire libre.', './hoteles/Gran_Hotel_Luna_de_Granada_Granada-España.jpg', 'https://www.delunahotels.com/en/granada/hotels/gran-hotel-luna-de-granada'),
   (25, 'Machu Picchu', './MachuPicchu.jpg', 'Perú', 'Español', 'Huayna Picchu', 'Cuy al horno', 'América', 'Inkaterra Machu Picchu Pueblo Hotel', 'Resort de lujo en Aguas Calientes, cerca de las ruinas de Machu Picchu, con habitaciones elegantes, piscinas, spa y restaurante gourmet.', './hoteles/HotelInkaterra-Machu-Picchu-Peru.jpg', 'https://www.inkaterra.com/'),
   (26, 'Chamonix', './Chamonix.jpg', 'Francia', 'Francés', 'Mont Blanc', 'La tartiflette', 'Europa', 'Hotel Le Morgane', 'Hotel de lujo en Chamonix, cerca del centro comercial, con habitaciones elegantes y vistas al Mont Blanc.', './Hotel_Le_Morgane-Chamonix-Francia.jpg', 'https://morgane-hotel-chamonix.com/en/'),
   (27, 'Los Angeles', './LosAngeles.jpg', 'EE.UU', 'Inglés', 'Parque Griffith', 'Hickory Burger', 'América', 'Hotel Figueroa - Unbound Collection by Hyatt', 'Hotel boutique en Downtown Los Ángeles con habitaciones modernas, spa, restaurante gourmet y vistas de la ciudad.', './hoteles/Hotel_Figueroa-Los-Angeles-Usa.jpg', 'https://www.hotelfigueroa.com/'),
   (28, 'Roma', './roma.jpg', 'Italia', 'Italiano', 'Palacio Barberini', 'Gnocchi', 'Europa', 'Hotel de Russie, A Rocco Forte Hotel', 'Hotel de lujo entre la Plaza de España y la Piazza del Popolo, conocido por su arquitectura, jardines y servicio. Habitaciones lujosas con camas king y baños de mármol.', './hoteles/Hotel-de-Russie-Roma-Italia.jpg', 'https://www.roccofortehotels.com/hotels-and-resorts/hotel-de-russie/'),
   (29, 'Toronto', './toronto.jpg', 'Canadá', 'Francés/Inglés', 'Torre CN', 'Poutine', 'América', 'Fairmont Royal York', 'Icónico hotel de lujo en el corazón de Toronto, con habitaciones elegantes, spa, restaurantes y vistas del centro.', './hoteles/Fairmont_Royal_York-Toronto-Canada.jpg', 'https://www.fairmont.com/royal-york-toronto/'),
   (30, 'Berlín', './berlin.jpg', 'Alemania', 'Alemán', 'Puerta de Brandeburgo', 'Eisbein', 'Europa', 'Hotel Adlon Kempinski', 'Hotel de lujo cerca de la Puerta de Brandenburgo con habitaciones elegantes, spa, restaurantes y vistas de la ciudad.', './hoteles/Hotel_Adlon_Kempinski-Berlin-Alemania.jpg', 'https://www.kempinski.com/en/hotel-adlon'),
   (31, 'Ciudad de México', './ciudadMexico.jpg', 'México', 'Español', 'Coyoacán', 'Chilaquiles', 'América', 'Four Seasons Hotel Mexico City', 'Hotel de lujo en Polanco, Ciudad de México, con habitaciones elegantes, spa, restaurantes y vistas de la ciudad.', './hoteles/Four_Seasons_Hotel-Ciudad-De-Mexico-Mexico.jpg', 'https://www.fourseasons.com/es/mexico/'),
   (32, 'Madrid', './madrid.jpg', 'España', 'Castellano', 'Palacio Real', 'Cocido Madrileño', 'Europa', 'Rosewood Villa Magna', 'Hotel de lujo en Paseo de la Castellana, con habitaciones lujosas, spa sublime y múltiples opciones de restaurantes.', './hoteles/Rosewood_Villa_Magna-Madrid-Espana.jpg', 'https://www.rosewoodhotels.com/es/villa-magna'),
   (33, 'Nueva York', './NuevaYork.jpg', 'EE.UU', 'Inglés', 'Central Park', 'Pizza', 'América', 'The Plaza Hotel', 'Icónico hotel de lujo en la Quinta Avenida y Calle 59, famoso por su arquitectura, historia y ambiente exclusivo.', './hoteles/The-Plaza-Hotel-Manhattan-New-York.jpg', 'https://www.theplazany.com/'),
   (34, 'París', './paris.jpg', 'Francia', 'Francés', 'Torre Eiffel', 'Foie gras', 'Europa', 'Le Bristol Paris', 'Hotel de lujo en Campos Elíseos, cerca del Arco de Triunfo y la Ópera, ofreciendo una estancia cómoda y sofisticada en París.', './hoteles/Le_Bristol_Paris-Francia.jpg', 'https://bit.ly/4g7IF2B'),
   (35, 'Miami', './miami.jpg', 'EE.UU', 'Inglés', 'Miami Beach', 'Pargo Frito y Griot', 'América', 'Kimpton EPIC Hotel', 'Hotel de lujo en el distrito financiero de Miami, con vistas del Río Miami y la Bahía Biscayne.', './hoteles/Kimpton_EPIC_Hotel-Miami.jpg', 'https://www.epichotel.com/'),
   (36, 'Viena', './viena.jpg', 'Austria', 'Alemán', 'Palacio de Schönbrunn', 'Wiener Schnitzel', 'Europa', 'Hotel Sans Souci Wien', 'Hotel boutique cerca del Barrio de los Museos, con habitaciones personalizadas, gimnasio, piscina y spa.', './hoteles/Hotel_Sans_Souci_Wien-Viena-Suiza.jpg', 'https://www.sanssouci-wien.com/'),
   (37, 'Chicago', './chicago.jpg', 'EE.UU', 'Inglés', 'Cloud Gate', 'Deep-dish Pizza', 'América', 'The Langham, Chicago', 'Hotel de lujo a orillas del río Chicago, cerca de la avenida Michigan, con habitaciones elegantes y vistas de la ciudad.', './hoteles/The_Langham_Chicago-Usa.jpg', 'https://www.langhamhotels.com/en/the-langham/chicago/'),
   (38, 'Londres', './londres.jpg', 'Reino Unido', 'Inglés', 'Abadía Westminster', 'Fish & Chips', 'Europa', 'The Ritz London, A JW Marriott Hotel', 'Icónico hotel de lujo en Mayfair, con habitaciones elegantes, spa, restaurantes, bares y vistas de la ciudad.', './hoteles/The_Ritz_London_Hotel-London-Uk.jpg', 'https://www.theritzlondon.com/'),
   (39, 'Bora Bora', './BoraBora.jpg', 'Polinesia Francesa', 'Francés', 'Otemanu', 'Roulottes', 'América', 'Hilton Hotel Tahiti', 'Hermoso hotel a cinco minutos del Aeropuerto Internacional Faaa, con amplia piscina y vistas a la isla Moorea.', './hoteles/Hilton_Hotel_Tahiti.jpg', 'https://bit.ly/3CGiwtd'),
   (40, 'Dubái', './dubai.jpg', 'Emiratos Árabes', 'Árabe', 'Burj Al Arab', 'El Mezze', 'Asia', 'Jumeirah Burj Al Arab', 'Hotel de lujo conocido por su forma de vela y ubicación en una isla artificial frente a la playa de Jumeirah.', './hoteles/Jumeirah_Burj_Al_Arab.jpg', 'https://www.jumeirah.com/en/stay/dubai/burj-al-arab-jumeirah');
   ```

## Licencia
Dominio Público.
