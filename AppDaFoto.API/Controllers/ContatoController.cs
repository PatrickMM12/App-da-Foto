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
            if (contato == null)
            {
                return BadRequest(new { errors = $"Contato Vazio" });
            }
            try
            {
                int response = _contatoRepositorio.AdicionarContato(contato);
                if (response == 0)
                {
                    return NotFound(new { errors = $"Contato do Fotografo de id={contato.IdFotografo} não adicionado" });
                }
                return CreatedAtAction(nameof(GetContatoPorId), new { id = contato.Id }, contato);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }
         
        [HttpPut("{id}")]
        public IActionResult PutContato(int id, [FromBody] Contato contato)
        {
            contato.Id = id;
            if (contato == null)
            {
                return BadRequest(new { errors = $"Contato do Fotografo de id={id} vazio" });
            }
            try
            {
                int response = _contatoRepositorio.AtualizarContato(contato);
                if (response == 0)
                {
                    return NotFound(new { errors = $"Contato do Fotografo de id={id} não atualizado" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public void DeleteContato(int id)
        {
            _contatoRepositorio.DeletarContato(id);
        }
    }
}
