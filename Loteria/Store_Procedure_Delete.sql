CREATE OR ALTER PROCEDURE spEliminarCarta (
  @id_carta INT -- Parámetro de entrada para el identificador de la carta a eliminar
)
AS
BEGIN
  DELETE FROM Celdas WHERE id_carta = @id_carta; -- Eliminar todas las celdas que hacen referencia a la carta a eliminar
  DELETE FROM Cartas WHERE id_carta = @id_carta; -- Eliminar la carta especificada por su identificador único
END;


CREATE OR ALTER PROCEDURE spEliminarCarta (
  @id_carta INT -- Parámetro de entrada para el identificador de la carta a eliminar
)
AS
BEGIN
  DELETE FROM Celdas WHERE id_carta = @id_carta; -- Eliminar todas las celdas que hacen referencia a la carta a eliminar
  DELETE FROM Cartas WHERE id_carta = @id_carta; -- Eliminar la carta especificada por su identificador único
END;


CREATE OR ALTER  PROCEDURE spEliminarCelda (
  @id_celda INT -- Parámetro de entrada para el identificador de la celda a eliminar
)
AS
BEGIN
  DELETE FROM Celdas WHERE id_celda = @id_celda; -- Eliminar la celda especificada por su identificador único
END;
