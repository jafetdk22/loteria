using System;
using System.Collections.Generic;

namespace loteria.Models.Entities;

public partial class Tableros
{
    public int IdTablero { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Celdas> Celdas { get; } = new List<Celdas>();
}
