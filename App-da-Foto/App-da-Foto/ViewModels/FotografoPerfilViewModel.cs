using App_da_Foto.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    [QueryProperty(nameof(FotografoId), nameof(FotografoId))]
    public class FotografoPerfilViewModel : BaseViewModel
    {
        private string fotografoId;
        private string nome;
        private string especialidade;
        public string Id { get; set; }

        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }

        public string Especialidade
        {
            get => especialidade;
            set => SetProperty(ref especialidade, value);
        }

        public string FotografoId
        {
            get
            {
                return fotografoId;
            }
            set
            {
                fotografoId = value;
                LoadFotografoId(value);
            }
        }

        public async void LoadFotografoId(string fotografoId)
        {
            try
            {
                var _fotografo = new Fotografo(); // OU await DataStore.GetFotografoAsync();


                var fotografos = await DataStore.GetFotografosAsync();
                foreach (var fotografo in fotografos)
                {
                    if (fotografo.Id == fotografoId)
                        _fotografo = fotografo;

                }

                Id = _fotografo.Id;
                Nome = _fotografo.Nome;
                Especialidade = _fotografo.Especialidade;
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha ao carregar fotografo");
            }
        }
    }
}
