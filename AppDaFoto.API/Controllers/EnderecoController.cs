using app_da_foto.Domain.Model;
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

        [HttpPost]
        public IActionResult AddEndereco(Endereco endereco)
        {
            _enderecoRepositorio.AdicionarEndereco(endereco);

            return CreatedAtAction(nameof(GetEnderecoPorId), new { id = endereco.Id }, endereco);
        }

        [HttpPut("{id}")]
        public void PutEndereco(int id, [FromBody] Endereco endereco)
        {
            endereco.Id = id;
            _enderecoRepositorio.AtualizarEndereco(endereco);
        }

        [HttpDelete("{id}")]
        public void DeleteEndereco(int id)
        {
            _enderecoRepositorio.DeletarEndereco(id);
        }
    }
}
