using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class FotografoRepositorio : IFotografoRepositorio
    {
        private List<Fotografo> _fotografos;

        public FotografoRepositorio()
        {
            
        }

        private void InicializaDados()
        {
            _fotografos = DalHelper.Fotografos();
        }

        public IEnumerable<Fotografo> BuscarTodosFotografos
        {
            get
            {
                return _fotografos;
            }
        }

        public List<Fotografo> BuscarFotografos(string nome, string especialidade)
        {
            return DalHelper.BuscarFotografos(nome, especialidade);
        }

        public Fotografo BuscarFotografoLogin(string email, string senha)
        {
            return DalHelper.BuscarFotografoLogin(email, senha);
        }

        public Fotografo BuscarFotografoPorId(int id)
        {
            return DalHelper.BuscarFotografoPorId(id);
        }

        public FotografoCompleto BuscarFotografoCompletoPorId(int id)
        {
            return DalHelper.BuscarFotografoCompletoPorId(id);
        }

        public int AdicionarFotografo(Fotografo fotografo)
        {
            if (fotografo == null)
            {
                throw new ArgumentNullException("fotografo");
            }
            return DalHelper.AdicionarFotografo(fotografo);
        }

        public int AtualizarFotografo(Fotografo fotografo)
        {
            if (fotografo == null)
            {
                throw new ArgumentNullException("fotografo");
            }
            return DalHelper.AtualizarFotografo(fotografo);
        }

        public void DeletarFotografo(int id)
        {
            DalHelper.DeletarFotografo(id);
        }

    }
}
