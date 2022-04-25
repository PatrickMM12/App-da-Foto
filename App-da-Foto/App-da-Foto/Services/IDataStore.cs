using System.Collections.Generic;
using System.Threading.Tasks;

namespace App_da_Foto.Services
{
	public interface IDataStore<T>
	{
		Task<bool> AddFotografoAsync(T fotografo);
		Task<bool> UpdateFotografoAsync(T fotografo);
		Task<bool> DeleteFotografoAsync(string id);
		Task<T> GetFotografoAsync(string id);
		Task<IEnumerable<T>> GetFotografosAsync(bool forceRefresh = false);
	}
}
