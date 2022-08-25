using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private List<Contato> _contatos;

        public ContatoRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _contatos = DalHelper.BuscarContatos();
        }

        public IEnumerable<Contato> BuscarTodosContatos
        {
            get
            {
                return _contatos;
            }
        }

        public Contato BuscarContatoPorId(int id)
        {
            return DalHelper.BuscarContatoPorId(id);
        }

        public void AdicionarContato(Contato contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException("contato");
            }
            DalHelper.AdicionarContato(contato);
        }

        public void AtualizarContato(Contato contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException("contato");
            }
            DalHelper.AtualizarContato(contato);
        }

        public void DeletarContato(int id)
        {
            DalHelper.DeletarContato(id);
        }

    }
}
