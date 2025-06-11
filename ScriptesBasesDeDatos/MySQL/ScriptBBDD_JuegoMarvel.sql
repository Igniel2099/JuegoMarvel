create Database JuegoMarvelBaseDeDatos;
use JuegoMarvelBaseDeDatos;

-- Tabla Personaje
CREATE TABLE Personaje (
    Id_personaje INT PRIMARY KEY AUTO_INCREMENT,
    NombreCompleto VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50),
    Grupo VARCHAR(50)
);

ALTER TABLE Personaje
ADD COLUMN Coste INT;



-- Tabla Habilidades
CREATE TABLE Habilidades (
    Id_habilidades INT PRIMARY KEY AUTO_INCREMENT,
    Id_personaje INT,
    Nombre VARCHAR(50),
    Valor INT,
    Tipo VARCHAR(50),
    FOREIGN KEY (Id_personaje) REFERENCES Personaje(Id_personaje)
);

-- Tabla PersonajeUsuario
CREATE TABLE PersonajeUsuario (
    Id_personajeUsuario INT PRIMARY KEY AUTO_INCREMENT,
    Id_personaje INT,
    Nivel INT DEFAULT 1,
    Valor_Habilidad1 INT DEFAULT 0,
    Valor_Habilidad2 INT DEFAULT 0,
    Valor_Habilidad3 INT DEFAULT 0,
    Id_equipo INT,
    Id_usuario INT
);

-- Tabla Equipo
CREATE TABLE Equipo (
    Id_equipo INT PRIMARY KEY AUTO_INCREMENT,
    Id_personajeUsuario1 INT,
    Id_personajeUsuario2 INT,
    Id_personajeUsuario3 INT,
    FOREIGN KEY (Id_personajeUsuario1) REFERENCES PersonajeUsuario(Id_personajeUsuario),
    FOREIGN KEY (Id_personajeUsuario2) REFERENCES PersonajeUsuario(Id_personajeUsuario),
    FOREIGN KEY (Id_personajeUsuario3) REFERENCES PersonajeUsuario(Id_personajeUsuario)
);

-- Tabla Usuario 
CREATE TABLE Usuario (
    Id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    NombreUsuario VARCHAR(50) NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
    Experiencia INT DEFAULT 0,
    Monedas INT DEFAULT 0,
    SuperPuntos INT DEFAULT 0,
    Id_equipo INT UNIQUE,
    FOREIGN KEY (Id_equipo) REFERENCES Equipo(Id_equipo)
);

ALTER TABLE Usuario
ADD COLUMN CodigoConfirmacion INT NULL
AFTER Contraseña;

ALTER TABLE PersonajeUsuario
ADD CONSTRAINT fk_personajeusuario_personaje
FOREIGN KEY (Id_personaje) REFERENCES Personaje(Id_personaje);

ALTER TABLE PersonajeUsuario
ADD CONSTRAINT fk_personajeusuario_equipo
FOREIGN KEY (Id_equipo) REFERENCES Equipo(Id_equipo);

ALTER TABLE PersonajeUsuario
ADD CONSTRAINT fk_personajeusuario_usuario
FOREIGN KEY (Id_usuario) REFERENCES Usuario(Id_usuario);

-- Tabla Peleas
CREATE TABLE Peleas (
    Id_peleas INT PRIMARY KEY AUTO_INCREMENT,
    PrimerUsuario INT,
    SegundoUsuario INT,
    Ganador INT,
    FOREIGN KEY (PrimerUsuario) REFERENCES Usuario(Id_usuario),
    FOREIGN KEY (SegundoUsuario) REFERENCES Usuario(Id_usuario),
    FOREIGN KEY (Ganador) REFERENCES Usuario(Id_usuario)
);

-- Tabla intermedia PeleasUsuario
CREATE TABLE PeleasUsuario (
    Id_usuario INT,
    Id_peleas INT,
    PRIMARY KEY (Id_usuario, Id_peleas),
    FOREIGN KEY (Id_usuario) REFERENCES Usuario(Id_usuario),
    FOREIGN KEY (Id_peleas) REFERENCES Peleas(Id_peleas)
);
