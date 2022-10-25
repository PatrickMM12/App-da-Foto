using app_da_foto.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Repositorio.Interfaces;
using System.Data.SqlClient;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotografoController : ControllerBase
    {
        private readonly IFotografoRepositorio _fotografoRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly IFotoRepositorio _fotoRepositorio;

        public FotografoController()
        {
            _fotografoRepositorio = new FotografoRepositorio();
            _enderecoRepositorio = new EnderecoRepositorio();
            _contatoRepositorio = new ContatoRepositorio();
            _fotoRepositorio = new FotoRepositorio();
        }

        [HttpGet]
        public IEnumerable<Fotografo> GetFotografos()
        {
            return _fotografoRepositorio.BuscarTodosFotografos;
        }

        [HttpGet]
        [Route("busca/")]
        public IActionResult GetFotografosBusca(string nome, string especialidade)
        {
            if (nome == "v@zio")
            {
                nome = "";
            }
            if (especialidade == "v@zio")
            {
                especialidade = "";
            }

            List<Fotografo> fotografos = _fotografoRepositorio.BuscarFotografos(nome, especialidade);

            if (fotografos == null)
            {
                return NotFound();
            }
            return new JsonResult(fotografos);
        }

        [HttpGet]
        [Route("login/")]
        public IActionResult GetFotografoLogin(string email, string senha)
        {
            try
            {
                Fotografo fotografo = _fotografoRepositorio.BuscarFotografoLogin(email, senha);

                if (fotografo == null)
                {
                    return NotFound();
                }
                return new JsonResult(fotografo);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFotografoPorId(int id)
        {
            try
            {
                FotografoCompleto fotografoCompleto = _fotografoRepositorio.BuscarFotografoCompletoPorId(id);

                if (fotografoCompleto == null)
                {
                    return NotFound();
                }
                return new JsonResult(fotografoCompleto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult AddFotografo(Fotografo fotografo)
        {
            int response = _fotografoRepositorio.AdicionarFotografo(fotografo);

            if(response == 0)
            {
                return NotFound(new { message = $"Fotografo não cadastrado" });
            }

            return CreatedAtAction(nameof(GetFotografoLogin), new { email = fotografo.Email, senha = fotografo.Senha }, fotografo);
        }

        [HttpPut("{id}")]
        public IActionResult PutFotografo(int id, [FromBody] Fotografo fotografo)
        {
            fotografo.Id = id;
            if (fotografo == null)
            {
                return BadRequest(new { message = $"Fotografo de id={id} vazio" });
            }
            try
            {
                int response = _fotografoRepositorio.AtualizarFotografo(fotografo);
                if (response == 0)
                {
                    return NotFound(new { message = $"Fotografo de id={id} não atualizado"});
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public void DeleteFotografo(int id)
        {
            _fotografoRepositorio.DeletarFotografo(id);
        }
    }
}
