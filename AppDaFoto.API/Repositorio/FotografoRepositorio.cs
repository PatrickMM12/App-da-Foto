using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class FotografoRepositorio : IFotografoRepositorio
    {
        private List<Fotografo> _fotografos;

        public FotografoRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _fotografos = DalHelper.BuscarFotografos();
        }

        public IEnumerable<Fotografo> BuscarTodosFotografos
        {
            get
            {
                return _fotografos;
            }
        }
        
        public Fotografo BuscarFotografo(string email, string senha)
        {
            return DalHelper.BuscarFotografo(email, senha);
        }

        public Fotografo BuscarFotografoPorId(int id)
        {
            return DalHelper.BuscarFotografoPorId(id);
        }

        public void AdicionarFotografo(Fotografo fotografo)
        {
            if (fotografo == null)
            {
                throw new ArgumentNullException("fotografo");
            }
            DalHelper.AdicionarFotografo(fotografo);
        }

        public void AtualizarFotografo(Fotografo fotografo)
        {
            if (fotografo == null)
            {
                throw new ArgumentNullException("fotografo");
            }
            DalHelper.AtualizarFotografo(fotografo);
        }

        public void DeletarFotografo(int id)
        {
            DalHelper.DeletarFotografo(id);
        }

    }
}
