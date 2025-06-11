use juegomarvelbasededatos;

-- Usuarios predeterminados
INSERT INTO Usuario (NombreUsuario, Correo, Contraseña, Experiencia, Monedas, SuperPuntos, Id_equipo)
VALUES ('walther', 'waltheralexander2025@gmail.com', 'Carbono143412_', 200, 3600, 10, NULL),
('Sentry','walthervallejo2018@gmail.com', 'Alexander1234567890_', 200, 3600, 10, NULL),
('Alexander','wallthervallejo2003@gmail.com', 'BuenasNoches1234567890_', 200, 3600, 10, NULL);

INSERT INTO Personaje(NombreCompleto, Tipo, Grupo, Coste)
VALUES ('Capitan America', 'Tank','Vengador', 1200),
('DareDevil', 'Striker', 'Defensor', 1200),
('Deadpool', 'Striker', 'X-Men', 1200),
('Elektra', 'Striker', 'Villano', 1200),
('Iron Fist', 'Striker', 'Defensor', 1200),
('Venom', 'Thank', 'Villano', 1200),
('Cyclops', 'Striker','X-Men', 1200);



INSERT INTO habilidades(Id_personaje, Nombre, Valor, Tipo)
VALUES(7, 'Rasho Masonico',10,'Ataque'),
(7, 'Rasho Lazer',10,'Ataque'),
(7, 'MMA con un Ojo',10,'Ataque');

INSERT INTO habilidades(Id_personaje, Nombre, Valor, Tipo)
VALUES(1, 'Puño de la Libertad',10,'Ataque'),
(1, 'Estampida America', 15,  'Ataque'),
(1, 'Escudo Libertario',13,'Ataque'),
(2, 'MMA masivo',10,'Ataque'),
(2, 'Patadon Historico',11,'Ataque'),
(2, 'Baritas Locas',9,'Ataque'),
(3, 'Romper 4 pared',4,'Ataque'),
(3, 'Espadazos Locos',10,'Ataque'),
(3, 'Oe pero colabora',12,'Ataque'),
(4, 'Copia de Rafaelo',10,'Ataque'),
(4, 'Damisela Kick',10,'Ataque'),
(4, 'Estampida Femenina',10,'Ataque'),
(5, 'La de Buda?',8,'Escudo'),
(5, 'Puño fortuito',14,'Ataque'),
(5, 'Patadon Bruce Lee',34,'Ataque'),
(6, 'El Venudo loco',90,'Curacion'),
(6, 'Garra de Negro',12,'Ataque'),
(6, 'A lo Venezolano',13,'Ataque');







