using app_da_foto.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repositorio;

namespace FotografosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotografosController : ControllerBase
    {
        private readonly IFotografoRepositorio _fotografoRepositorio;

        public FotografosController()
        {
            this._fotografoRepositorio = new FotografoRepositorio();
        }


        [HttpGet]
        [Route("todos/")]
        public IEnumerable<Fotografo> GetFotografos()
        {
            return _fotografoRepositorio.BuscarTodosFotografos;
        }

        [HttpGet]
        public IActionResult GetFotografo(string email, string senha)
        {
            Fotografo fotografo = _fotografoRepositorio.BuscarFotografo(email, senha);

            if(fotografo == null)
            {
                return NotFound();
            }
            return new JsonResult(fotografo);
        }

        [HttpGet("{id}")]
        public Fotografo GetFotografoPorId(int id)
        {
            return _fotografoRepositorio.BuscarFotografoPorId(id);
        }

        [HttpPost]
        public IActionResult AddFotografo(Fotografo fotografo)
        {
            _fotografoRepositorio.AdicionarFotografo(fotografo);

            return CreatedAtAction(nameof(GetFotografo), new { email = fotografo.Email, senha = fotografo.Senha }, fotografo);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Fotografo fotografo)
        {
            fotografo.Id = id;
            _fotografoRepositorio.AtualizarFotografo(fotografo);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _fotografoRepositorio.DeletarFotografo(id);
        }
    }
}
