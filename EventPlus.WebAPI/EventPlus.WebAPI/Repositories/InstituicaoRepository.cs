using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    // Injeção de dependência: recebe o contexto pelo construtor
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza uma instituição usando rastreamento automático do EF Core
    /// </summary>
    /// <param name="id">Id da instituição a ser atualizada</param>
    /// <param name="tipoInstituicao">Novos dados da instituição</param>
    public void Atualizar(Guid id, Instituicao Instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            //Atualiza os campos do tipo de instituicao buscado com os novos dados
            instituicaoBuscada.NomeFantasia = Instituicao.NomeFantasia;

            //Detecta a mudança na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Busca uma instituição por id
    /// </summary>
    /// <param name="id">Id da instituição a ser buscada</param>
    /// <returns>Objeto Instituicao ou null se não encontrado</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }




    /// <summary>
    /// Cadastra uma nova instituição
    /// </summary>
    /// <param name="tipoInstituicao">Instância da instituição a ser cadastrada</param>
    public void Cadastrar(Instituicao tipoInstituicao)
    {
        _context.Instituicaos.Add(tipoInstituicao);
        _context.SaveChanges();
    }




    /// <summary>
    /// Deleta uma instituição
    /// </summary>
    /// <param name="id">Id da instituição a ser deletada</param>
    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }




    /// <summary>
    /// Lista todas as instituições cadastradas
    /// </summary>
    /// <returns>Lista de Instituicao</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos
            .OrderBy(tipoInstituicao => tipoInstituicao.NomeFantasia) //Ordena os tipos de eventos por título
            .ToList();
    }
}