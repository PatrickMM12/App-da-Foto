using app_da_foto.Domain.Model;

namespace Repositorio.Interfaces
{
    public interface IFotoRepositorio
    {
        IEnumerable<Foto> BuscarTodasFotos { get; }

        List<Foto> BuscarFotoPorId(int id);

        Foto BuscarFotoPerfil(int id);

        int AdicionarFoto(Foto foto);

        int AtualizarFoto(Foto foto);

        void DeletarFoto(int id);
    }
}
