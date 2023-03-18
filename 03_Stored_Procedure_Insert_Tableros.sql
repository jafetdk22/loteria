USE [Loteria]
GO
CREATE OR ALTER PROCEDURE spAgregarTablero (
  @nombre VARCHAR(255), -- Par�metro de entrada para el nombre del tablero
  @descripcion VARCHAR(255) -- Par�metro de entrada para la descripci�n del tablero
)
AS
BEGIN
  INSERT INTO Tableros (nombre, descripcion) -- Insertar los valores proporcionados en la tabla Tableros
  VALUES (@nombre, @descripcion);
END;
