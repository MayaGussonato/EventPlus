using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("TipoEvento")]
public partial class TipoEvento
{
    [Key]
    public Guid IdTipoEvento { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [InverseProperty("IdtipoEventoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
