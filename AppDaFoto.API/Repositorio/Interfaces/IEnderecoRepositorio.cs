using app_da_foto.Domain.Model;
using App_da_Foto.Models;

namespace Repositorio.Interfaces
{
    public interface IEnderecoRepositorio
    {
        IEnumerable<Endereco> BuscarTodosEnderecos { get; }

        Endereco BuscarEnderecoPorId(int id);

        List<EnderecoFotografo> BuscarEnderecosPorLocalizacao(double latitudeSul, double latitudeNorte, double longitudeOeste, double longitudeLeste);

        int AdicionarEndereco(Endereco endereco);

        int AtualizarEndereco(Endereco endereco);

        void DeletarEndereco(int id);
    }
}
