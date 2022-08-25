using app_da_foto.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Repositorio.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotoController : ControllerBase
    {
        private readonly IFotoRepositorio _fotoRepositorio;

        public FotoController()
        {
            _fotoRepositorio = new FotoRepositorio();
        }

        [HttpGet]
        public IEnumerable<Foto> GetFotos()
        {
            return _fotoRepositorio.BuscarTodasFotos;
        }

        [HttpGet("{id}")]
        public IActionResult GetFotoPorId(int id)
        {
            Foto foto = _fotoRepositorio.BuscarFotoPorId(id);

            if (foto == null)
            {
                return NotFound();
            }
            return new JsonResult(foto);
        }

        [HttpPost]
        public IActionResult AddFoto(Foto foto)
        {
            _fotoRepositorio.AdicionarFoto(foto);

            return CreatedAtAction(nameof(GetFotoPorId), new { id = foto.Id }, foto);
        }

        [HttpPut("{id}")]
        public void PutFoto(int id, [FromBody] Foto foto)
        {
            foto.Id = id;
            _fotoRepositorio.AtualizarFoto(foto);
        }

        [HttpDelete("{id}")]
        public void DeleteFoto(int id)
        {
            _fotoRepositorio.DeletarFoto(id);
        }
    }
}
