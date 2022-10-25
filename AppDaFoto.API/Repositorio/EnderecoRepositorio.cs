using app_da_foto.Domain.Model;
using App_da_Foto.Models;
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

        public List<EnderecoFotografo> BuscarEnderecosPorLocalizacao(double latitudeSul, double latitudeNorte, double longitudeOeste, double longitudeLeste)
        {
            return DalHelper.BuscarEnderecosPorLocalizao(latitudeSul, latitudeNorte, longitudeOeste, longitudeLeste);
        }

        public Endereco BuscarEnderecoPorId(int id)
        {
            return DalHelper.BuscarEnderecoPorId(id);
        }

        public int AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("Endereço Vazio");
            }
            return DalHelper.AdicionarEndereco(endereco);
        }

        public int AtualizarEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("Endereço Vazio");
            }
            return DalHelper.AtualizarEndereco(endereco);
        }

        public void DeletarEndereco(int id)
        {
            DalHelper.DeletarEndereco(id);
        }

    }
}
