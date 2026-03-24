using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public void Atualizar(Guid id, Presenca presencaAtualizada)
    {
        var PresencaBuscada = _eventContext.Presencas.Find(id);

        if (PresencaBuscada != null) // Verifica se a presença existe antes de tentar atualizar
        {
            PresencaBuscada.Situacao = !PresencaBuscada.Situacao; // Alterna a situação da presença
            _eventContext.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }



    /// <summary>
    /// Busca por uma presenca por Id
    /// </summary>
    /// <param name="id">id da  presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var PresencaBuscada = _eventContext.Presencas.Find(id);

        if (PresencaBuscada != null) // Verifica se a presença existe antes de tentar deletar
        {
            _eventContext.Presencas.Remove(PresencaBuscada);
            _eventContext.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    public void Inscrever(Presenca Inscricao)
    {
        _eventContext.Presencas.Add(Inscricao);
        _eventContext.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .ToList();
    }



    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presencas de um usuario especifico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}