USE [TP_Final_Programacion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SELECT NEWID()
SELECT LEN('DCB2685E-CC04-43CA-998B-26CF97145A9F')

--- Tabla Categoria
CREATE TABLE Categoria (
IdCategoria int IDENTITY (1,1) PRIMARY KEY,
NombreCategoria varchar (200) NOT NULL,
DescripcionCategoria varchar (900) NOT NULL
)
GO


--Tabla Libros
CREATE TABLE Libros (
IdLibro int IDENTITY (1,1) PRIMARY KEY,
IdCategoria int NOT NULL, ----------- COLUMNA PARA REFERENCIAR A CATEGORIA
IdAutor int NOT NULL, ---- COLUMNA PARA REFERENCIAR AL AUTOR DEL LIBRO
Nombre varchar (300) NOT NULL,
Descripcion varchar (600) NOT NULL,
Precio decimal (18,2) NOT NULL,
Stock int NULL,
AniodePublicacion datetime NOT NULL,
Imagen varchar (400) NOT NULL

--- Incluir la relacion con categoria
   FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria),

---- Incluir la relacion con autor
FOREIGN KEY (IdAutor) REFERENCES Autores(IdAutor)
)
GO


---Tabla Usuarios
CREATE TABLE Usuarios (
	IdUsuario int IDENTITY (1,1) PRIMARY KEY,
	Nombre varchar(100) NOT NULL,
	Email varchar(400) NOT NULL,
	Contrasenia varchar(400) NOT NULL,
	CategoriaPreferida varchar(200) NOT NULL,
	IdRol varchar(50) NOT NULL,
	Activo bit NOT NULL,
) 
GO

-- Tabla Autores
CREATE TABLE Autores (
IdAutor int IDENTITY (1,1) PRIMARY KEY,
NombreAutor varchar (200) NOT NULL,
Biografia varchar (900) NOT NULL,
AnioNacimiento datetime NOT NULL,
AnioFallecimiento datetime NULL,
)
GO

-- Tabla Carrito
CREATE TABLE Carrito (
    IdCarrito int IDENTITY (1,1) PRIMARY KEY NOT NULL,
    IdUsuario int NOT NULL,    -- Columna para referenciar a Usuarios
    PrecioTotalCarrito decimal (18,2) NOT NULL,
    FechaCreacion datetime NOT NULL,
    Descripcion varchar(100) NOT NULL,
	Estado bit NOT NULL,

    -- REFERENCIA A LAS TABLAS USUARIOS Y PRODUCTOS
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
)
GO

--Detalle Carrito
CREATE TABLE DetalleCarrito(
IdDetalleCarrito int IDENTITY (1,1) PRIMARY KEY NOT NULL,
PrecioTotalDetalleCarrito decimal (18,2) NOT NULL,
IdCarrito int NOT NULL,
FechaFactura datetime,
IdLibro int NOT NULL,  -- Columna para referenciar a Productos
DetalleFactura varchar(800) NOT NULL,
FechaCreacionFactura datetime NOT NULL,

--Referencia al carrito al que pertenece
FOREIGN KEY (IdCarrito) REFERENCES Carrito (IdCarrito),

--Referencia a que producto hay en el carrito
FOREIGN KEY (IdLibro) REFERENCES Libros(IdLibro)
)
GO

--TABLA VALORACION DE USUARIOS
CREATE TABLE ValoraciondeUsuarios(
IdValoración int IDENTITY (1,1) PRIMARY KEY NOT NULL,
IdUsuario int NOT NULL, ---Columna para referenciar a que usuario pertenece
IdLibro int NOT NULL, --Columna para refenciar a que libro pertenece la valoración
Valoracion varchar (900) NOT NULL,
Puntuacion int, --- Aclarar en la APP con texto, que es de 1 a 5
FechadeValoracion datetime NOT NULL,

--Referencia al Libro que pertenece
FOREIGN KEY (IdLibro) REFERENCES Libros (IdLibro),

--Referencia al usuario que pertenece
FOREIGN KEY (IdUsuario) REFERENCES Usuarios (IdUsuario),
)
GO

CREATE TABLE Notas (
    IdNota int IDENTITY (1,1) PRIMARY KEY,
    IdUsuario int NOT NULL,          -- Usuario que crea la nota
    IdLibro int NOT NULL,            -- Libro relacionado con la nota
    TextoNota varchar(900) NOT NULL, -- Contenido de la nota

    -- Relaciones con Usuarios y Libros
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdLibro) REFERENCES Libros(IdLibro)
)
GO


----------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Adrian Solmira', 'solmiraadrian@gmail.com','Solmira!82A$', 'Drama', 0, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Camila Dreval', 'camidre232@gmail.com', 'Cami@D92v#L', 'Suspenso', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Lucía Forcani', 'forcanil@yahoo.com', 'LFor$!82ci@', 'Cómics', 0, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Martin Celviano', 'celvianotincho@outlook', 'C3lv!@nMart1', 'Ciencia Ficción', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Paula Tenovik', 'paulat93@gmail.com', 'PauTen0@v!K$', 'Novela', 0, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Santiago Orvendel', 'orvesanti@yahoo.com', 'Orv3nS@t!9oG', 'Terror', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Ana Klaviner', 'kana@hotmail.com', 'AnaK$la9v@iN', 'Comedia', 0, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Valeria Jorenzo', 'jorenzovale@yahoo.com', 'Jor3nz@L!Va$', 'Drama', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Nicolas Pradek', 'pradek91n@gmail.com', 'NPrade!2l@s9', 'Ciencia Ficción', 0, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, CategoriaPreferida, IdRol, Activo)
VALUES('Florencia Malseran', 'flormalse@yahoo.com', 'Mal$erFl0@ciA', 'Manga', 1, 1);

---------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Drama', 'Género literario que explora las emociones humanas y los conflictos interpersonales. Aborda temas profundos como el amor, la pérdida, la moralidad y las relaciones humanas, destacándose por su intensidad emocional. Las historias suelen basarse en situaciones realistas que invitan a la reflexión y conectan al lector con las experiencias de los personajes.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Suspenso', 'Género que atrapa al lector con una sensación constante de expectativa y misterio. Caracterizado por giros inesperados, secretos ocultos y un clima de tensión creciente. Su propósito es mantener al lector intrigado hasta el desenlace, con narrativas que exploran crímenes, conspiraciones o dilemas psicológicos.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Cómics', ' Historias visuales narradas a través de ilustraciones y diálogos. Varían en estilo y temática, desde aventuras de superhéroes hasta dramas cotidianos o sátiras políticas. Combinan arte y narrativa para ofrecer una experiencia inmersiva y accesible, adaptada a lectores de todas las edades.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Ciencia Ficción', 'Género que especula sobre futuros posibles, tecnologías avanzadas o escenarios alternativos. Mezcla ciencia y ficción para explorar temas como viajes espaciales, inteligencia artificial, realidades virtuales o mundos distópicos. Invita a reflexionar sobre el impacto de la ciencia en la humanidad.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Novela', 'Extensas narrativas que desarrollan personajes complejos, tramas elaboradas y múltiples subtramas. Su formato versátil permite explorar cualquier género, desde la ficción histórica hasta el romance o la fantasía. Son una forma literaria que profundiza en las emociones y pensamientos de sus personajes.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Terror', 'Género diseñado para provocar miedo y tensión. Incluye elementos sobrenaturales, psicológicos o grotescos para explorar los temores más profundos de los personajes y los lectores. Crea atmósferas inquietantes que juegan con lo desconocido y el suspenso.');

INSERT INTO Categoria (NombreCategoria, DescripcionCategoria)
VALUES('Manga', 'Historias gráficas de origen japonés que abarcan diversos géneros, como acción, romance, fantasía o comedia. Reconocidos por su estilo artístico característico, se leen de derecha a izquierda. Capturan narrativas profundas o ligeras, con un enfoque visual impactante.');


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

DBCC CHECKIDENT ('Autores', RESEED, 0);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Dan Brown', 'Daniel Gerhard Brown es un autor estadounidense conocido por sus novelas de suspense, entre las que se incluyen las novelas de Robert Langdon Ángeles y demonios, El código Da Vinci, El símbolo perdido, Infierno y Origen. Sus novelas son búsquedas de tesoros que suelen transcurrir durante un período de 24 horas.', 1964-06-22);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Paula Hawkins', 'Paula Hawkins es una autora británica conocida por su exitosa novela de suspenso psicológico La chica del tren, que trata temas como la violencia doméstica, el alcohol y el abuso de drogas. La novela fue adaptada al cine en 2016, protagonizada por Emily Blunt.', 1972-08-26);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Gillian Flynn', 'Gillian Schieber Flynn es una autora, guionista y productora estadounidense, mejor conocida por sus novelas de suspenso y misterio Sharp Objects, Lugares Oscuros y Perdida, todas las cuales han recibido elogios de la crítica.', 1971-02-24);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Thomas Harris', 'William Thomas Harris III es un escritor estadounidense. Es autor de una serie de novelas de suspenso sobre Hannibal Lecter.',1940-09-22);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Ken Follet', 'Kenneth Martin Follett es un autor galés de novelas históricas y de suspenso que ha vendido más de 160 millones de copias de sus obras. El gran éxito comercial de Follett llegó con el thriller de espías Eye of the Needle.', 1949-06-05);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('John Katzenbach', 'John Katzenbach es un autor estadounidense de ficción popular, Katzenbach trabajó como reportero de tribunales penales para el Miami Herald y el Miami News y como escritor destacado para la revista Tropic del Herald.', 1950-06-23);

INSERT INTO Autores(NombreAutor, Biografia, AnioFallecimiento, AnioNacimiento)
VALUES('Carlos Ruíz Zafón', 'Carlos Ruiz Zafón fue un novelista español conocido por su novela de 2001 La sombra del viento. La novela vendió 15 millones de copias y fue ganadora de numerosos premios', 2020-06-19, 1964-09-25);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Joël Dicker', 'Joël Dicker es un escritor suizo nacido el 16 de junio de 1985 en Ginebra. Es conocido por sus novelas de suspenso, destacándose La verdad sobre el caso Harry Quebert, que le valió reconocimiento internacional y premios como el Grand Prix de l’Académie Française. Su estilo combina intriga, profundidad psicológica y narrativas complejas.', 1985-06-16);

INSERT INTO Autores(NombreAutor, Biografia, AnioFallecimiento, AnioNacimiento)
VALUES('Daphne Du Maurier', 'Daphne Du Maurier fue una escritora británica conocida por sus novelas de misterio y suspenso. Autora de obras icónicas como Rebecca y Jamaica Inn, su estilo combina intriga psicológica y atmósferas góticas. Proveniente de una familia artística, su legado literario ha influido en el cine y la literatura moderna.', 1989-04-19, 1907-05-13);

INSERT INTO Autores(NombreAutor, Biografia, AnioNacimiento)
VALUES('Dennis Lehane','Dennis Lehane es un escritor estadounidense, reconocido por sus novelas de misterio y crimen, es autor de obras como Mystic River, Gone Baby Gone y Shutter Island, que han sido adaptadas al cine. Sus historias, a menudo ambientadas en Boston, destacan por su profundidad psicológica y tramas complejas.', 1965-08-04);

---------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('El código Da Vinci', 5, 1, 2003-01-01, 200, 'Un thriller que sigue a Robert Langdon, un simbologista que se ve involucrado en un asesinato en el Museo del Louvre. A medida que investiga, descubre una conspiración que se remonta a los primeros días del cristianismo.', 12500, 'C:\Users\Joaquin\Desktop\TP\codigodavinci.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('La chica del tren', 1, 2, 2015-01-06, 120, 'La historia se centra en Rachel, una mujer que se obsesiona con la vida de una pareja que observa desde el tren. Cuando la mujer desaparece, Rachel se convierte en una testigo clave, pero su propia vida está llena de secretos oscuros.', 10000, 'C:\Users\Joaquin\Desktop\TP\lachicadeltren.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('Perdida', 2, 3, 2012-05-24, 40, 'En el día de su quinto aniversario, Nick Dunne reporta la desaparición de su esposa, Amy. A medida que la investigación avanza, se revelan verdades inquietantes sobre su matrimonio y los oscuros secretos de ambos.', 9800, 'C:\Users\Joaquin\Desktop\TP\perdida.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('El silencio de los inocentes', 2, 4, 1991-06-06, 500, 'La agente del FBI Clarice Starling busca la ayuda del encarcelado asesino en serie Hannibal Lecter para capturar a otro asesino. La historia explora la psicología del crimen y la manipulación.', 11200, 'C:\Users\Joaquin\Desktop\TP\elsilenciodelosinocentes.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('Los pilares de la tierra', 4, 5, 1989-10-01, 200, 'Ambientada en la Inglaterra medieval, la novela narra la historia de la construcción de una catedral en un pueblo y las luchas de poder entre nobles, religiosos y ciudadanos. Un drama lleno de intrigas y personajes complejos.', 11200, 'C:\Users\Joaquin\Desktop\TP\lospilaresdelatierra.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('El psicoanalista',6 , 6, 2002-01-29, 10, 'El Dr. Frederick Starks recibe un mensaje amenazante de un misterioso atacante que le da un mes para descubrir su identidad. A medida que intenta salvar su vida, se adentra en un juego psicológico peligroso.', 15000, 'C:\Users\Joaquin\Desktop\TP\elpsicoanalista.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('La sombre del viento', 1, 7, 2002-04-01, 20, 'Daniel, un joven en la Barcelona de la posguerra, descubre un libro en un misterioso cementerio de libros olvidados. A medida que investiga la vida del autor, se enfrenta a un oscuro secreto que amenaza su vida.', 11500, 'C:\Users\Joaquin\Desktop\TP\lasombradelviento.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('La verdad sobre el caso Harry Quebert', 2, 8, 2012-09-19, 200,'El escritor Marcus Goldman investiga la condena de su mentor Harry Quebert por el asesinato de una joven. A medida que desentraña la historia, descubre secretos que cambian su vida y la percepción de su mentor.', 10500, 'C:\Users\Joaquin\Desktop\TP\laverdaddelcasoharryquebert.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('Rebecca', 5, 9, 1938-08-22, 200, 'La historia sigue a una joven que se casa con el viudo Maxim de Winter, pero se siente opacada por la presencia de su primera esposa, Rebecca. La atmósfera gótica y el misterio que rodea a Rebecca la llevan a una inquietante revelación.', 40000, 'C:\Users\Joaquin\Desktop\TP\rebecca.jpeg');

INSERT INTO Libros (Nombre,IdCategoria, IdAutor, AniodePublicacion, Stock, Descripcion, Precio, Imagen)
VALUES ('Shutter Island', 5, 10, 2003-04-15, 100, 'En 1954, el mariscal de EE. UU. Teddy Daniels investiga la desaparición de una paciente en un hospital psiquiátrico en una isla remota. A medida que profundiza en el caso, comienza a cuestionar su propia cordura y la naturaleza de la verdad.', 8900, 'C:\Users\Joaquin\Desktop\TP\shutterisland.jpeg');

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Carrito(IdUsuario, Descripcion, FechaCreacion, PrecioTotalCarrito, Estado)
VALUES(2, 'Compra de "Rebecca" por $40.000', 2024-11-01, 40000, 1);

--------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO DetalleCarrito(PrecioTotalDetalleCarrito, IdCarrito, FechaFactura, IdLibro, DetalleFactura, FechaCreacionFactura)
VALUES(40000, 1, 2024-11-01, 9, 'Libro "Rebecca - Daphne Du Maurier"', 2024-11-01);

----------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO ValoraciondeUsuarios(IdUsuario, IdLibro, Valoracion, Puntuacion)
VALUES(2, 9, 'A pesar de la antigüedad, tiene excelente trama y te invita a leerlo, atrapandote desde el primer momento', 4);

------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Notas (IdUsuario, IdLibro, TextoNota)
VALUES (2, 9, 'Excelente libro, me hizo reflexionar mucho');