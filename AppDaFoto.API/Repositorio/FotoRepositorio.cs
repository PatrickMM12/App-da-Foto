using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class FotoRepositorio : IFotoRepositorio
    {
        private List<Foto> _fotos;

        public FotoRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _fotos = DalHelper.BuscarFotos();
        }

        public IEnumerable<Foto> BuscarTodasFotos
        {
            get
            {
                return _fotos;
            }
        }

        public Foto BuscarFotoPorId(int id)
        {
            return DalHelper.BuscarFotoPorId(id);
        }

        public void AdicionarFoto(Foto foto)
        {
            if (foto == null)
            {
                throw new ArgumentNullException("foto");
            }
            DalHelper.AdicionarFoto(foto);
        }

        public void AtualizarFoto(Foto foto)
        {
            if (foto == null)
            {
                throw new ArgumentNullException("foto");
            }
            DalHelper.AtualizarFoto(foto);
        }

        public void DeletarFoto(int id)
        {
            DalHelper.DeletarFoto(id);
        }

    }
}
