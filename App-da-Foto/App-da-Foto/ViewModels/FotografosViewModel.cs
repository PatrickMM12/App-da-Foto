using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.Views;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class FotografosViewModel : BaseViewModel
    {
        private string nome;
        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }

        private string especialidade;
        public string Especialidade
        {
            get => especialidade;
            set => SetProperty(ref especialidade, value);
        }

        private Fotografo _fotografoSelecionado;
        public Fotografo FotografoSelecionado
        {
            get => _fotografoSelecionado;
            set
            {
                SetProperty(ref _fotografoSelecionado, value);
                OnFotografoSelecionado(value);
            }
        }

        private ObservableCollection<Fotografo> fotografos;
        public ObservableCollection<Fotografo> Fotografos 
        { 
            get => fotografos;
            set => SetProperty(ref fotografos, value);
        }

        public Command LoadFotografosCommand { get; }
        public Command<Fotografo> FotografoTapped { get; }

        public FotografosViewModel()
        {
            FotografoService = new FotografoService();
            Fotografos = new ObservableCollection<Fotografo>();
            LoadFotografosCommand = new Command(async () => await ExecuteLoadFotografosCommand());

            FotografoTapped = new Command<Fotografo>(OnFotografoSelecionado);
        }

        public void OnAparecendo()
        {
            IsBusy = false;
            FotografoSelecionado = null;
        }

        async Task ExecuteLoadFotografosCommand()
        {
            Analytics.TrackEvent("Buscar Fotografos");

            IsBusy = true;
            NoResult = false;
            try
            {
                Fotografos.Clear();

                ResponseService<IEnumerable<Fotografo>> responseService = await FotografoService.ObterFotografos(Nome, Especialidade);

                if (responseService.IsSuccess)
                {
                    Fotografos = new ObservableCollection<Fotografo>(responseService.Data.OrderBy(item => item.Nome).ToList());
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", responseService.StatusCode.ToString() + responseService.Errors.ToString(), "OK");
                }

                if (Fotografos.Count == 0)
                {
                    NoResult = true;
                }
                else
                {
                    NoResult = false;
                }
            }
            catch (Exception erro)
            {
                await Shell.Current.DisplayAlert("Erro", erro.ToString(), "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnFotografoSelecionado(Fotografo fotografo)
        {
            if (fotografo == null)
                return;
            await Shell.Current.Navigation.PushPopupAsync(new Loading());

            await Shell.Current.GoToAsync($"{nameof(FotografoPerfilPage)}?{nameof(FotografoPerfilViewModel.FotografoId)}={fotografo.Id}");
            
        }
    }
}