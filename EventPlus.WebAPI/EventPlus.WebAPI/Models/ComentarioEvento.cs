using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("ComentarioEventos")]
public partial class ComentarioEvento
{
    [Key]
    public Guid IdComentarioEventos { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public bool Exibe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataComentarioEvento { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdEvento { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("ComentarioEventos")]
    [JsonIgnore]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("ComentarioEventos")]
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
