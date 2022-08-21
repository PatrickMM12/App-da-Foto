using app_da_foto.Domain.Model;

namespace Repositorio
{
    public interface IFotografoRepositorio
    {
        IEnumerable<Fotografo> BuscarTodosFotografos { get; }

        Fotografo BuscarFotografoPorId(int id);

        Fotografo BuscarFotografo(string email, string senha);

        void AdicionarFotografo(Fotografo fotografo);

        void AtualizarFotografo(Fotografo fotografo);

        void DeletarFotografo(int id);
    }
}
