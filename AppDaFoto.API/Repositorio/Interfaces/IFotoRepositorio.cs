using app_da_foto.Domain.Model;

namespace Repositorio.Interfaces
{
    public interface IFotoRepositorio
    {
        IEnumerable<Foto> BuscarTodasFotos { get; }

        Foto BuscarFotoPorId(int id);

        void AdicionarFoto(Foto foto);

        void AtualizarFoto(Foto foto);

        void DeletarFoto(int id);
    }
}
