using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;

    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos
        .FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var ComentarioEventoBuscado = _context.ComentarioEventos.Find(id);

        if (ComentarioEventoBuscado != null) // Verifica se o tipo de evento existe antes de tentar deletar
        {
            _context.ComentarioEventos.Remove(ComentarioEventoBuscado); //remove o tipo de evento encontrado
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos
        .Include(e => e.IdEventoNavigation) // Inclui os dados do evento relacionado
        .Include(e => e.IdUsuarioNavigation)
        .ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
        .Where(c => c.IdEvento == IdEvento && c.Exibe == true) // Filtra os comentários para o evento específico e que estão marcados para exibição
        .Include(c => c.IdUsuarioNavigation) //
        .ToList();
    }
}