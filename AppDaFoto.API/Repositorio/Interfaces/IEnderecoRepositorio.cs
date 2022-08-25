using app_da_foto.Domain.Model;

namespace Repositorio.Interfaces
{
    public interface IEnderecoRepositorio
    {
        IEnumerable<Endereco> BuscarTodosEnderecos { get; }

        Endereco BuscarEnderecoPorId(int id);

        void AdicionarEndereco(Endereco endereco);

        void AtualizarEndereco(Endereco endereco);

        void DeletarEndereco(int id);
    }
}
