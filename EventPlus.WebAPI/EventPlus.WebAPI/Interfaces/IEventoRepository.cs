using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    void Deletar(Guid id);
    List<Evento> Listar();
    void Atualizar(Guid id, Evento evento);
    List<Evento> ListarPorTipo(Guid Id);
    List<Evento> ProximosEventos();
    Evento BuscarPorId(Guid id);
}