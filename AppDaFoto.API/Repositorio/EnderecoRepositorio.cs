using app_da_foto.Domain.Model;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private List<Endereco> _enderecos;

        public EnderecoRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _enderecos = DalHelper.BuscarEnderecos();
        }

        public IEnumerable<Endereco> BuscarTodosEnderecos
        {
            get
            {
                return _enderecos;
            }
        }

        public Endereco BuscarEnderecoPorId(int id)
        {
            return DalHelper.BuscarEnderecoPorId(id);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("endereco");
            }
            DalHelper.AdicionarEndereco(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("endereco");
            }
            DalHelper.AtualizarEndereco(endereco);
        }

        public void DeletarEndereco(int id)
        {
            DalHelper.DeletarEndereco(id);
        }

    }
}
