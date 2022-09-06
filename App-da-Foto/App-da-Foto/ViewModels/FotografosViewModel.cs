using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class FotografosViewModel : BaseViewModel
    {
        private Fotografo _fotografoSelecionado;
        private FotografoService _fotografoService;

        public ObservableCollection<Fotografo> Fotografos { get; }
        public Command LoadFotografosCommand { get; }
        public Command<Fotografo> FotografoTapped { get; }

        public FotografosViewModel()
        {
            Title = "Listagem de Fotógrafos";
            Fotografos = new ObservableCollection<Fotografo>();
            LoadFotografosCommand = new Command(async () => await ExecuteLoadFotografosCommand());

            FotografoTapped = new Command<Fotografo>(OnFotografoSelecionado);

        }

        async Task ExecuteLoadFotografosCommand()
        {
            IsBusy = true;

            try
            {
                Fotografos.Clear();
                //var fotografos = await DataStore.GetFotografosAsync();
                var fotografos = await _fotografoService.ObterFotografos();
                foreach (var fotografo in fotografos)
                {
                    Fotografos.Add(fotografo);
                }
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAparecendo()
        {
            IsBusy = false;
            FotografoSelecionado = null;
        }

        public Fotografo FotografoSelecionado
        {
            get => _fotografoSelecionado;
            set
            {
                SetProperty(ref _fotografoSelecionado, value);
                OnFotografoSelecionado(value);
            }
        }



        async void OnFotografoSelecionado(Fotografo fotografo)
        {
            if (fotografo == null)
                return;

            // This will push the FotografoPerfilPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FotografoPerfilPage)}?{nameof(FotografoPerfilViewModel.FotografoId)}={fotografo.Id}");
        }
    }
}