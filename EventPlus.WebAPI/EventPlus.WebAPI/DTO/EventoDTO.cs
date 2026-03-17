namespace EventPlus.WebAPI.DTOs;

public class EventoDTO
{
    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataEvento { get; set; }

    public string Descricao { get; set; } = null!;

    public Guid? IdtipoEvento { get; set; }

    public Guid? IdInstituicao { get; set; }
}