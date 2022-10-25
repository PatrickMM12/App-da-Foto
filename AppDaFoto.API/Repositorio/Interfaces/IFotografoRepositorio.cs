using app_da_foto.Domain.Model;

namespace Repositorio.Interfaces
{
    public interface IFotografoRepositorio
    {
        IEnumerable<Fotografo> BuscarTodosFotografos { get; }

        List<Fotografo> BuscarFotografos(string nome, string especialidade);

        Fotografo BuscarFotografoPorId(int id);

        FotografoCompleto BuscarFotografoCompletoPorId(int id);

        Fotografo BuscarFotografoLogin(string email, string senha);

        int AdicionarFotografo(Fotografo fotografo);

        int AtualizarFotografo(Fotografo fotografo);

        void DeletarFotografo(int id);
    }
}
