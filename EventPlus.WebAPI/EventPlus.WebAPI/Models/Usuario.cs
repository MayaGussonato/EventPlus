using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__A9D1053427F560A4", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;

    public Guid? IdTipoUsuario { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdTipoUsuario")]
    [InverseProperty("Usuarios")]
    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
