using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.Views;
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
            string email = Email;
            string senha = Senha;

            if (email == null || senha == null)
            {
                await Shell.Current.DisplayAlert("Erro!", "Campo de E-mail e/ou senha vazio(s)!", "OK");
                return;
            }
            await Shell.Current.Navigation.PushPopupAsync(new Loading());

            ResponseService<Fotografo> responseService = await FotografoService.ObterFotografo(email, senha);

            if (responseService.IsSuccess)
            {
                App.Current.Properties.Add("Fotografo", JsonConvert.SerializeObject(responseService.Data));
                await App.Current.SavePropertiesAsync();

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
                    await Shell.Current.DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais Tarde!", "OK");
                }
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
