USE [Loteria]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarCelda]    Script Date: 17/03/2023 10:17:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertarCelda]
    @id_carta INT,
    @id_tablero INT,
    @fila INT,
    @columna INT
AS
BEGIN
    INSERT INTO Celdas ( id_carta, id_tablero, fila, columna)
    VALUES ( @id_carta, @id_tablero, @fila, @columna);
END
