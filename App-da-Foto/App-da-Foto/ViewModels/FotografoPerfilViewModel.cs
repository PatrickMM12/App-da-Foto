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

        private string endereco;
        public string Endereco
        {
            get => endereco;
            set => SetProperty(ref endereco, value);
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
                    Endereco = responseService.Data.Endereco.Logradouro + ", " + responseService.Data.Endereco.Numero;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", responseService.StatusCode.ToString() + responseService.Errors.ToString(), "OK");
                }
            }
            catch (Exception erro)
            {
                await Shell.Current.DisplayAlert("Erro", erro.ToString(), "Ok");
            }
            finally
            {
                await Shell.Current.Navigation.PopAllPopupAsync();
            }
        }

        private async void OnWhatsAppCliked()
        {
            Analytics.TrackEvent("WhatsApp Clique");
            await Shell.Current.Navigation.PushPopupAsync(new Loading());
            try
            {
                if (Telefone != null & Telefone != "")
                {
                    Chat.Open(Telefone, "Olá, vim pelo App Da Foto! Me fale um pouco mais sobre seu trabalho!");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", "Número de WhatsApp não informado", "Ok");
                }
            }
            catch
            {
                await Shell.Current.DisplayAlert("Erro", "WhatsApp não instalado! Instale o WhatsApp e tente novamente!", "Ok");
            }
            await Shell.Current.Navigation.PopAllPopupAsync();
        }

        private async void OnInstagramCliked(object obj)
        {
            Analytics.TrackEvent("Instagram Clique");
            await Shell.Current.Navigation.PushPopupAsync(new Loading());
            try
            {
                if (Instagram != null & Instagram != "")
                {
                    Uri uri = new Uri(Instagram);
                    await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", "Link de Instagram não informado", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message.ToString(), "Ok");
            }
            await Shell.Current.Navigation.PopAllPopupAsync();
        }
    }
}
