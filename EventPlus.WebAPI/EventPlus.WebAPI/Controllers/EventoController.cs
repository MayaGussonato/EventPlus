using EventPlus.WebAPI.DTOs;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo listar eventos filtando pelo id do usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que faz a chamadaa para o metodo de listar os proximos eventos
    /// </summary>
    /// <returns>Statua code 200 e a lista dos proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
           
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que lista todos os eventos
    /// </summary>
    /// <returns>Status code 200 e lista de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que busca um evento pelo ID
    /// </summary>
    /// <param name="id">Id do evento</param>
    /// <returns>Status code 200 e o evento encontrado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_eventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para um método cadastrar um evento
    /// </summary>
    /// <param name="evento">evento a ser cadastrado</param>
    /// <returns>Status code 201 e o evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(EventoDTO evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome,
                Descricao = evento.Descricao,
                DataEvento = evento.DataEvento,
                IdtipoEvento = evento.IdtipoEvento,
                IdInstituicao = evento.IdInstituicao
            };

            _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201, novoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que atualiza um evento existente
    /// </summary>
    /// <param name="id">Id do evento</param>
    /// <param name="evento">Novos dados do evento</param>
    /// <returns>Status code 204</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, Evento evento)
    {
        try
        {
            _eventoRepository.Atualizar(id, evento);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que deleta um evento
    /// </summary>
    /// <param name="id">Id do evento</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
