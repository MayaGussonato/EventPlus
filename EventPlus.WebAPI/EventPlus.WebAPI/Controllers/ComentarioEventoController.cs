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
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(IComentarioEventoRepository comentarioEventoRepository)
    {
        _comentarioEventoRepository = comentarioEventoRepository;
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

    [HttpPost]
    public IActionResult Cadastrar(ComentarioEventoDTO comentarioevento)
    {
        try
        {
            var novoComentarioEvento = new ComentarioEvento
            {
                Descricao = comentarioevento.Descricao!,
                Exibe = comentarioevento.Exibe!,
                DataComentarioEvento = comentarioevento.DataComentarioEvento!,
                IdUsuario = comentarioevento.IdUsuario!,
                IdEvento = comentarioevento.IdEvento!
            };

            _comentarioEventoRepository.Cadastrar(novoComentarioEvento);
            return StatusCode(201, novoComentarioEvento);
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