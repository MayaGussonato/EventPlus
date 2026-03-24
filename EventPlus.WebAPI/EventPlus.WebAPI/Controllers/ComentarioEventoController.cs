using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

        public ComentarioEventoController(ContentSafetyClient 
            contentSafetyClient, IComentarioEventoRepository 
            comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }
    /// <summary>
    /// Endponit da API que cadastra e modera um comentario
    /// </summary>
    /// <param name="comentarioevento">comentario a ser moderado</param>
    /// <returns>Status code 201 e o comentario criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar (ComentarioEventoDTO comentarioevento)
    {
        try 
        {
          if (string.IsNullOrEmpty(comentarioevento . Descricao))
          {
            return BadRequest("O Texto a ser moderado nao pode estar vazio.");
          }

            //criar objeto de analise
            var request = new AnalyzeTextOptions(comentarioevento . Descricao);

            //chamar a Api do azure content safety
            Response<AnalyzeTextResult> response = await 
                _contentSafetyClient.AnalyzeTextAsync(request);

            // verificar se o texto tem alguma severidade maio que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentario =new ComentarioEvento
            {
                Descricao = comentarioevento.Descricao,
                IdUsuario = comentarioevento.IdUsuario,
                IdEvento = comentarioevento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comenttario vai ser exibido
                Exibe = !temConteudoImproprio 
            };

            //cadastrar o comentario
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Evento/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("{idUsuario}/{idEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento) // Busca um comentário específico de um usuário para um evento específico
    {
        try
        {
            var comentario = _comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento); // Chama o método do repositório para buscar o comentário com base no ID do usuário e do evento

            if (comentario != null) // Verifica se o comentário foi encontrado
                return NotFound(); // Retorna 404 se o comentário não for encontrado

            return Ok(comentario); // Retorna 200 com o comentário encontrado
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
}