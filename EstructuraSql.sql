-- Base de datos
CREATE DATABASE AquaSaveDB;
GO
USE AquaSaveDB;
GO

-- Tabla Usuarios
CREATE TABLE Usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombreCompleto NVARCHAR(100) NOT NULL,
    correo NVARCHAR(100) UNIQUE NOT NULL,
    contrasena NVARCHAR(100) NOT NULL,
    rol NVARCHAR(20) CHECK (rol IN ('admin','user'))
);
GO

-- Tabla Retos
CREATE TABLE Retos(
    id INT IDENTITY(1,1) PRIMARY KEY,
    titulo NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255),
    puntos INT NOT NULL CHECK (puntos >= 0),
    tipo NVARCHAR(50) CHECK (tipo IN ('diario','semanal','especial')),
    dificultad NVARCHAR(20) CHECK (dificultad IN ('facil','media','dificil')),
);
GO

-- Tabla Participaciones (relación N:N entre Usuarios y Retos)
CREATE TABLE Participaciones (
    id INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NOT NULL,
    idReto INT NOT NULL,
    fechaAsignacion DATETIME DEFAULT GETDATE(),
    completado BIT DEFAULT 0,
    FOREIGN KEY (idUsuario) REFERENCES Usuarios(id),
    FOREIGN KEY (idReto) REFERENCES Retos(id)
);
GO

-- Tabla HistorialPuntos
CREATE TABLE HistorialPuntos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NOT NULL,
    puntosGanados INT NOT NULL,
    fechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (idUsuario) REFERENCES Usuarios(id)
);
GO

-- 🔹 Datos semilla
INSERT INTO Usuarios (nombreCompleto, correo, contrasena)
VALUES 
('Admin AquaSave', 'admin@aquasave.com', '1234'),
('María López', 'maria@correo.com', 'abcd'),
('Carlos Ruiz', 'carlos@correo.com', 'pass123');

INSERT INTO Retos (titulo, descripcion, puntos, tipo, dificultad)
VALUES
('Ducha de 5 minutos', 'Reducir el tiempo de baño a 5 minutos', 10, 'diario', 'facil'),
('Lavado eficiente', 'Lavar ropa solo 3 veces por semana', 20, 'semanal', 'media'),
('Cosecha de agua lluvia', 'Recolectar y reutilizar agua lluvia', 30, 'especial', 'dificil');

INSERT INTO Participaciones (idUsuario, idReto, completado)
VALUES
(2, 1, 1),
(3, 2, 0);

INSERT INTO HistorialPuntos (idUsuario, puntosGanados)
VALUES
(2, 10),
(3, 0);
GO