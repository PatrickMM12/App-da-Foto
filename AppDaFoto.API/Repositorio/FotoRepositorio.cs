using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class FotoRepositorio : IFotoRepositorio
    {
        private List<Foto> _fotos;

        public FotoRepositorio()
        {
            
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

        public List<Foto> BuscarFotoPorId(int id)
        {
            return DalHelper.BuscarFotoPorId(id);
        }

        public Foto BuscarFotoPerfil(int id)
        {
            return DalHelper.BuscarFotoPerfil(id);
        }

        public int AdicionarFoto(Foto foto)
        {
            if (foto == null)
            {
                throw new ArgumentNullException("foto");
            }
            return DalHelper.AdicionarFoto(foto);
        }

        public int AtualizarFoto(Foto foto)
        {
            if (foto == null)
            {
                throw new ArgumentNullException("foto");
            }
            return DalHelper.AtualizarFoto(foto);
        }

        public void DeletarFoto(int id)
        {
            DalHelper.DeletarFoto(id);
        }

    }
}
