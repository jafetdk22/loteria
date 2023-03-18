-- Tabla para almacenar las cartas de la lotería
CREATE TABLE Cartas (
  id_carta INT PRIMARY KEY, -- Identificador único de la carta
  imagen VARCHAR(255) NOT NULL, -- Ruta o enlace a la imagen de la carta
  descripcion VARCHAR(255) NOT NULL -- Descripción de la imagen de la carta
);

-- Tabla para almacenar los tableros de la lotería
CREATE TABLE Tableros (
  id_tablero INT PRIMARY KEY, -- Identificador único del tablero
  nombre VARCHAR(255) NOT NULL, -- Nombre del tablero
  descripcion VARCHAR(255) NOT NULL -- Descripción del tablero
);

-- Tabla para almacenar las celdas de los tableros y las cartas asignadas a ellas
CREATE TABLE Celdas (
  id_celda INT PRIMARY KEY, -- Identificador único de la celda
  id_carta INT NOT NULL, -- Identificador de la carta asignada a la celda
  id_tablero INT NOT NULL, -- Identificador del tablero al que pertenece la celda
  fila INT NOT NULL, -- Fila de la celda en el tablero
  columna INT NOT NULL, -- Columna de la celda en el tablero
  FOREIGN KEY (id_carta) REFERENCES Cartas(id_carta), -- Clave foránea para asegurarse de que la carta existe
  FOREIGN KEY (id_tablero) REFERENCES Tableros(id_tablero) -- Clave foránea para asegurarse de que el tablero existe
);


-- Las columnas de todas las tablas tienen tipos de datos explícitos, lo cual es una buena práctica para evitar confusiones.
-- Se han definido claves primarias y claves foráneas para mantener la integridad de los datos.
-- Se ha utilizado una longitud de cadena adecuada para las columnas de tipo VARCHAR.
-- Sería útil agregar índices si se espera que las tablas tengan muchos registros para mejorar la velocidad de búsqueda.
-- Se podría considerar agregar campos adicionales, como una columna "activo" para permitir que los usuarios desactiven cartas o tableros en lugar de eliminarlos por completo de la Base de Datos.
