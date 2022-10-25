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
            List<Foto> foto = _fotoRepositorio.BuscarFotoPorId(id);

            if (foto == null)
            {
                return NotFound();
            }
            return new JsonResult(foto);
        }

        [HttpGet]
        [Route("perfil/")]
        public IActionResult GetFotoPerfil(int id)
        {
            Foto foto = _fotoRepositorio.BuscarFotoPerfil(id);

            if (foto == null)
            {
                return NotFound();
            }
            return new JsonResult(foto);
        }

        [HttpPost]
        public IActionResult AddFoto(Foto foto)
        {
            if (foto == null)
            {
                return BadRequest(new { errors = $"Foto Vazia!" });
            }
            try
            {
                int response = _fotoRepositorio.AdicionarFoto(foto);
                if (response == 0)
                {
                    return NotFound(new { errors = $"Foto do Fotografo de id={foto.IdFotografo} não adicionado" });
                }
                return CreatedAtAction(nameof(GetFotoPorId), new { id = foto.Id }, foto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutFoto(int id, [FromBody] Foto foto)
        {
            foto.Id = id;

            if (foto == null)
            {
                return BadRequest(new { errors = $"Foto do Fotografo vazia" });
            }
            try
            {
                int response = _fotoRepositorio.AtualizarFoto(foto);
                if (response == 0)
                {
                    return NotFound(new { errors = $"Foto do Fotografo de id={foto.Fotografo.Id} não atualizado" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public void DeleteFoto(int id)
        {
            _fotoRepositorio.DeletarFoto(id);
        }
    }
}
