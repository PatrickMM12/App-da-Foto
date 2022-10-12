using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;

namespace App_da_Foto.ViewModels
{
    [QueryProperty(nameof(FotografoId), nameof(FotografoId))]
    public class FotografoPerfilViewModel : BaseViewModel
    {
        private int fotografoId;
        public int FotografoId
        {
            get
            {
                return fotografoId;
            }
            set
            {
                fotografoId = value;
                LoadFotografoId(fotografoId);
            }
        }

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

        private string telefone;
        public string Telefone
        {
            get => telefone;
            set => SetProperty(ref telefone, value);
        }

        private string instagram;
        public string Instagram
        {
            get => instagram;
            set => SetProperty(ref instagram, value);
        }

        public FotografoPerfilViewModel()
        {
            FotografoService = new FotografoService();

            WhatsAppCommand = new Command(OnWhatsAppCliked);
            InstagramCommand = new Command(OnInstagramCliked);
        }


        public Command WhatsAppCommand { get; }
        public Command InstagramCommand { get; }

        public async void LoadFotografoId(int id)
        {
            Analytics.TrackEvent("Carregar Perfil Fotografo");
            try
            {
                Fotografo _fotografo = new Fotografo();

                ResponseService<FotografoCompleto> responseService = await FotografoService.ObterFotografo(id);

                if (responseService.IsSuccess)
                {
                    Nome = responseService.Data.Fotografo.Nome;
                    Especialidade = responseService.Data.Fotografo.Especialidade;

                    Telefone = responseService.Data.Contato.Telefone;
                    Instagram = responseService.Data.Contato.Instagram;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", responseService.StatusCode.ToString() + responseService.Errors.ToString(), "OK");
                }

                //IEnumerable<Fotografo> fotografos = await FotografoService.ObterFotografos();
                //_fotografo = fotografos.FirstOrDefault(a => a.Id == FotografoId);

                //foreach (var fotografo in fotografos)
                //{
                //    if (fotografo.Id == fotografoId)
                //        _fotografo = fotografo;
                //}

                //FotografoId = _fotografo.Id;

                //Nome = _fotografo.Nome;
                //Especialidade = _fotografo.Especialidade;

                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            catch (Exception erro)
            {
                await Shell.Current.DisplayAlert("Erro", erro.ToString(), "Ok");
            }
        }

        private async void OnWhatsAppCliked()
        {
            try
            {
                Chat.Open(Telefone, "Olá, vim pelo App Da Foto! Me fale um pouco mais sobre seu trabalho!");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message.ToString(), "Ok");
            }
        }

        private async void OnInstagramCliked(object obj)
        {
            try
            {
                Uri uri = new Uri(Instagram);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message.ToString(), "Ok");
            }
        }
    }
}
