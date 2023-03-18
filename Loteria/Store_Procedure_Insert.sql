USE [Loteria]
GO
CREATE OR ALTER PROCEDURE spAgregarTablero (
  @nombre VARCHAR(255), -- Parámetro de entrada para el nombre del tablero
  @descripcion VARCHAR(255) -- Parámetro de entrada para la descripción del tablero
)
AS
BEGIN
  INSERT INTO Tableros (nombre, descripcion) -- Insertar los valores proporcionados en la tabla Tableros
  VALUES (@nombre, @descripcion);
END;
