using app_da_foto.Domain.Model;

namespace Repositorio.Interfaces
{
    public interface IContatoRepositorio
    {
        IEnumerable<Contato> BuscarTodosContatos { get; }

        Contato BuscarContatoPorId(int id);

        void AdicionarContato(Contato contato);

        void AtualizarContato(Contato contato);

        void DeletarContato(int id);
    }
}
