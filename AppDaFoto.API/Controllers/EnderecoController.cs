using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Repositorio.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        public EnderecoController()
        {
            _enderecoRepositorio = new EnderecoRepositorio();
        }


        [HttpGet]
        [Route("todos/")]
        public IEnumerable<Endereco> GetEnderecos()
        {
            return _enderecoRepositorio.BuscarTodosEnderecos;
        }

        [HttpGet("{id}")]
        public IActionResult GetEnderecoPorId(int id)
        {
            Endereco endereco = _enderecoRepositorio.BuscarEnderecoPorId(id);

            if (endereco == null)
            {
                return NotFound();
            }
            return new JsonResult(endereco); 
        }

        [HttpGet]
        [Route("por-localizacao/")]
        public IActionResult GetEnderecosPorLocalizao(double latitudeSul, double latitudeNorte, double longitudeOeste, double longitudeLeste)
        {
            try
            {
                IEnumerable<EnderecoFotografo> enderecoFotografos = _enderecoRepositorio.BuscarEnderecosPorLocalizacao(latitudeSul, latitudeNorte, longitudeOeste, longitudeLeste);

                if (enderecoFotografos == null)
                {
                    return NotFound(new {message = "Nenhum fotógrafo encontrado"});
                }
                return new JsonResult(enderecoFotografos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult AddEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                return BadRequest(new { message = $"Endereco Vazio" });
            }
            try
            {
                int response = _enderecoRepositorio.AdicionarEndereco(endereco);
                if (response == 0)
                {
                    return NotFound(new { message = $"Endereco do Fotografo de id={endereco.IdFotografo} não adicionado" });
                }
                return CreatedAtAction(nameof(GetEnderecoPorId), new { id = endereco.Id }, endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutEndereco(int id, [FromBody] Endereco endereco)
        {
            endereco.Id = id;
            if (endereco == null)
            {
                return BadRequest(new { message = $"Endereco do Fotografo de id={id} vazio" });
            }
            try
            {
                int response = _enderecoRepositorio.AtualizarEndereco(endereco);
                if (response == 0)
                {
                    return NotFound(new { message = $"Endereco do Fotografo de id={id} não atualizado" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public void DeleteEndereco(int id)
        {
            _enderecoRepositorio.DeletarEndereco(id);
        }
    }
}
