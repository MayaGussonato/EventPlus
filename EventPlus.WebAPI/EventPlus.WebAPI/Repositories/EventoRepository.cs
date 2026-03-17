using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza os dados de um evento existente
    /// </summary>
    /// <param name="id">Identificador único do evento</param>
    /// <param name="evento">Objeto contendo os novos dados do evento</param>
    public void Atualizar(Guid id, Evento evento)
    {
        Evento? eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento;
            eventoBuscado.IdtipoEvento = evento.IdtipoEvento;
            eventoBuscado.IdInstituicao = evento.IdInstituicao;

            _context.Eventos.Update(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um evento pelo seu identificador
    /// </summary>
    /// <param name="id">Id único do evento</param>
    /// <returns>Evento encontrado ou nulo caso não exista</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos
            .Include(e => e.IdtipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .FirstOrDefault(e => e.IdEvento == id)!;
    }

    /// <summary>
    /// Cadastra um novo evento
    /// </summary>
    /// <param name="evento">Objeto contendo os dados do evento</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um evento existente
    /// </summary>
    /// <param name="id">Id único do evento</param>
    public void Deletar(Guid id)
    {
        Evento? eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista todos os eventos cadastrados
    /// </summary>
    /// <returns>Lista de eventos</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
            .Include(e => e.IdtipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .ToList();
    }

    /// <summary>
    /// Metodo que busca os eventos que o usuario confirmou presença
    /// </summary>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdtipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }

    /// <summary>
    /// Metodo que traz a lista de proximos eventos
    /// </summary>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos
            .Include(e => e.IdtipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}