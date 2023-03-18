using System;
using System.Collections.Generic;

namespace loteria.Models.Entities;

public partial class Cartas
{
    public int IdCarta { get; set; }

    public string Imagen { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Celdas> Celdas { get; } = new List<Celdas>();
}
