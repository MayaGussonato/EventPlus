using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        /// <summary>
        /// Lista todas as instituições
        /// </summary>
        /// 


        [HttpGet]
       public IActionResult Listar()
       {
            try
           {
                return Ok(_instituicaoRepository.Listar());
            }
            catch (Exception erro)
           {
               return BadRequest(erro.Message);
           }
       }

        /// <summary>
        /// Busca uma instituição pelo Id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Cadastra uma instituição
        /// </summary>
        [HttpPost]
        public IActionResult Post(InstituicaoDTO instituicao)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco= instituicao.Endereco!
                };

                _instituicaoRepository.Cadastrar(novaInstituicao);

                return StatusCode(201, novaInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Atualiza uma instituição
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizada = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!
                };

                _instituicaoRepository.Atualizar(id, instituicaoAtualizada);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Deleta uma instituição
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _instituicaoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}