using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFotografoRepositorio<T>
    {
        Task<bool> AddFotografoAsync(T fotografo);
        Task<bool> UpdateFotografoAsync(T fotografo);
        Task<bool> DeleteFotografoAsync(string id);
        Task<T> GetFotografoAsync(string id);
        Task<IEnumerable<T>> GetFotografosAsync(bool forceRefresh = false);
    }
}
