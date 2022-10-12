using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.Views;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string senha;

        public string Email 
        { 
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Senha 
        { 
            get => senha;
            set => SetProperty(ref senha, value);
        }

        public LoginViewModel()
        {
            FotografoService = new FotografoService();

            LoginCommand = new Command(OnLoginClicked);
            LoginFotografoCommand = new Command(OnLoginFotografoClicked);
            AddFotografoCommand = new Command(OnAdicionarFotografo);
        }

        public Command LoginCommand { get; }
        public Command LoginFotografoCommand { get; }
        public Command AddFotografoCommand { get; }

        private async void OnLoginFotografoClicked(object obj)
        {
            Analytics.TrackEvent("Login Fotografo");
            await Shell.Current.Navigation.PushPopupAsync(new Loading());
            if (Email == string.Empty || Senha == string.Empty || Email == null || Senha == null)
            {
                await Shell.Current.DisplayAlert("Erro!", "Campo de E-mail e/ou senha vazio(s)!", "OK");
                return;
            }

            try
            {
                ResponseService<Fotografo> responseService = await FotografoService.ObterFotografo(email, senha);

                if (responseService.IsSuccess)
                {
                    Fotografo fotografo = new Fotografo()
                    {
                        Id = responseService.Data.Id,
                        Nome = responseService.Data.Nome,
                        Especialidade = responseService.Data.Especialidade,
                        Email = responseService.Data.Email,
                    };

                    App.Current.Properties["Fotografo"] = JsonConvert.SerializeObject(fotografo);
                    await App.Current.SavePropertiesAsync();
                    Logado = true;

                    App.Current.MainPage = new HomePage();
                }
                else
                {
                    if (responseService.StatusCode == 404)
                    {
                        await Shell.Current.DisplayAlert("Erro!", "E-mail e/ou senha incorreto(s)!", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais Tarde! Erro: " + responseService.Errors.ToString() , "OK");
                    }
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais Tarde! Erro: " + ex.Message.ToString(), "OK");
            }
            await Shell.Current.Navigation.PopAllPopupAsync();
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnAdicionarFotografo(object obj)
        {
            await Shell.Current.GoToAsync("NovoFotografoPage");
        }
    }
}
