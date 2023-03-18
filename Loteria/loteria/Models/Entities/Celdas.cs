using System;
using System.Collections.Generic;

namespace loteria.Models.Entities;

public partial class Celdas
{
    public int IdCelda { get; set; }

    public int IdCarta { get; set; }

    public int IdTablero { get; set; }

    public int Fila { get; set; }

    public int Columna { get; set; }

    public virtual Cartas IdCartaNavigation { get; set; } = null!;

    public virtual Tableros IdTableroNavigation { get; set; } = null!;
}
