using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static EventPlus.WebAPI.DTO.LoginDTO;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Endpoit da API que faz a chamada para o metodo de buscar um usuario pelo id
        /// </summary>
        /// <param name="id">id do usuario a ser buscado</param>
        /// <returns>status code 200 e o usuario buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_usuarioRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da api faz a chamada para o metodo cadastrar um usuario
        /// </summary>
        /// <param name="usuario">Usuario a ser cadastrado</param>
        /// <returns>Status code 201 e o usuario cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(UsuarioDTO usuario)
        {
            try
            {
                var novoUsuario = new Usuario
                {
                    Nome = usuario.Nome!,
                    Senha = usuario.Senha!,
                    Email = usuario.Email!,
                    IdTipoUsuario = usuario.IdTipoUsuario
                };

                _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201, novoUsuario);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}