using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    //Injecao de dependência
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }



    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var TipoUsuarioExistente = _context.TipoUsuarios.Find(id);

        if (TipoUsuarioExistente != null)
        {
            //Atualiza os campos do tipo de evento buscado com os novos dados
            TipoUsuarioExistente.Titulo = tipoUsuario.Titulo;

            //Detecta a mudança na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }

    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var TipoUsuarioExistente = _context.TipoUsuarios.Find(id);

        if (TipoUsuarioExistente != null)
        {
            _context.TipoUsuarios.Remove(TipoUsuarioExistente);
            _context.SaveChanges();
        }
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario.Titulo).ToList();
    }
}