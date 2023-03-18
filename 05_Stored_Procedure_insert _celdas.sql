USE [Loteria]
GO
CREATE OR ALTER PROCEDURE sp_InsertarCelda
    @id_carta INT,
    @id_tablero INT,
    @fila INT,
    @columna INT
AS
BEGIN
    INSERT INTO Celdas ( id_carta, id_tablero, fila, columna)
    VALUES ( @id_carta, @id_tablero, @fila, @columna);
END
