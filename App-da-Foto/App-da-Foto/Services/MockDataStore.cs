using App_da_Foto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_da_Foto.Services
{
	public class MockDataStore : IDataStore<Fotografo>
	{
		readonly List<Fotografo> fotografos;

		public MockDataStore()
		{
			fotografos = new List<Fotografo>()
			{
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Patrick", Especialidade="Geral" },
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Thayna", Especialidade="Gestante" },
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Nicolly", Especialidade="Feminino" },
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Victor", Especialidade="Masculino" },
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Rafael", Especialidade="Newborn" },
				new Fotografo { Id = Guid.NewGuid().ToString(), Nome = "Severino", Especialidade="Animais" }
			};
		}

		public async Task<bool> AddFotografoAsync(Fotografo item)
		{
			fotografos.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateFotografoAsync(Fotografo item)
		{
			var oldFotografo = fotografos.Where((Fotografo arg) => arg.Id == item.Id).FirstOrDefault();
			fotografos.Remove(oldFotografo);
			fotografos.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteFotografoAsync(string id)
		{
			var oldFotografo = fotografos.Where((Fotografo arg) => arg.Id == id).FirstOrDefault();
			fotografos.Remove(oldFotografo);

			return await Task.FromResult(true);
		}

		public async Task<Fotografo> GetFotografoAsync(string id)
		{
			return await Task.FromResult(fotografos.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Fotografo>> GetFotografosAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(fotografos);
		}
	}
}