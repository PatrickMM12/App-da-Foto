using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private List<Contato> _contatos;

        public ContatoRepositorio()
        {
            
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

        public int AdicionarContato(Contato contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException("Contato Vazio");
            }
            return DalHelper.AdicionarContato(contato);
        }

        public int AtualizarContato(Contato contato)
        {
            if (contato == null)
            {
                throw new ArgumentNullException("Contato Vazio");
            }
            return DalHelper.AtualizarContato(contato);
        }

        public void DeletarContato(int id)
        {
            DalHelper.DeletarContato(id);
        }

    }
}
