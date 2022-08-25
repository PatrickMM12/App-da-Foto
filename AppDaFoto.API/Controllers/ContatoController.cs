using app_da_foto.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Repositorio.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController()
        {
            _contatoRepositorio = new ContatoRepositorio();
        }


        [HttpGet]
        public IEnumerable<Contato> GetContatos()
        {
            return _contatoRepositorio.BuscarTodosContatos;
        }

        [HttpGet("{id}")]
        public IActionResult GetContatoPorId(int id)
        {
            Contato contato = _contatoRepositorio.BuscarContatoPorId(id);

            if (contato == null)
            {
                return NotFound();
            }
            return new JsonResult(contato);
        }

        [HttpPost]
        public IActionResult AddContato(Contato contato)
        {
            _contatoRepositorio.AdicionarContato(contato);

            return CreatedAtAction(nameof(GetContatoPorId), new { id = contato.Id }, contato);
        }
         
        [HttpPut("{id}")]
        public void PutContato(int id, [FromBody] Contato contato)
        {
            contato.Id = id;
            _contatoRepositorio.AtualizarContato(contato);
        }

        [HttpDelete("{id}")]
        public void DeleteContato(int id)
        {
            _contatoRepositorio.DeletarContato(id);
        }
    }
}
