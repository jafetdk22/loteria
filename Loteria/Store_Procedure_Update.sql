CREATE OR ALTER PROCEDURE spActualizarCarta (
  @id_carta INT, -- Par�metro de entrada para el identificador de la carta a actualizar
  @imagen VARCHAR(255), -- Par�metro de entrada para la nueva ruta o enlace de la imagen de la carta
  @descripcion VARCHAR(255) -- Par�metro de entrada para la nueva descripci�n de la imagen de la carta
)
AS
BEGIN
  UPDATE Cartas -- Actualizar los valores de la tabla Cartas
  SET imagen = @imagen, descripcion = @descripcion
  WHERE id_carta = @id_carta; -- Limitar la actualizaci�n a la carta especificada por su identificador �nico
END;


CREATE OR ALTER PROCEDURE spActualizarTablero (
  @id_tablero INT, -- Par�metro de entrada para el identificador del tablero a actualizar
  @nombre VARCHAR(255), -- Par�metro de entrada para el nuevo nombre del tablero
  @descripcion VARCHAR(255) -- Par�metro de entrada para la nueva descripci�n del tablero
)
AS
BEGIN
  UPDATE Tableros -- Actualizar los valores de la tabla Tableros
  SET nombre = @nombre, descripcion = @descripcion
  WHERE id_tablero = @id_tablero; -- Limitar la actualizaci�n al tablero especificado por su identificador �nico
END;


CREATE OR ALTER PROCEDURE spActualizarCartaDeCelda (
  @id_celda INT, -- Par�metro de entrada para el identificador de la celda a actualizar
  @id_carta INT -- Par�metro de entrada para el nuevo identificador de la carta asignada a la celda
)
AS
BEGIN
  UPDATE Celdas -- Actualizar los valores de la tabla Celdas
  SET id_carta = @id_carta
  WHERE id_celda = @id_celda; -- Limitar la actualizaci�n a la celda especificada por su identificador �nico
END;
