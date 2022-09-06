using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.ViewModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_da_Foto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private FotografoService _fotografoService;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            _fotografoService = new FotografoService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (App.Current.Properties.ContainsKey("Fotografo"))
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        private async void Encontre_Fotografo(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private void Cadastrar_Fotografo(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("NovoFotografoPage");
        }
        
        private async void Login_Fotografo(object sender, EventArgs e)
        {
            string email = entryEmail.Text;
            string senha = entrySenha.Text;

            if(email == null || senha == null)
            {
                await DisplayAlert("Erro!", "Campo de E-mail e/ou senha vazio(s)!", "OK");
                return;
            }
            await Navigation.PushPopupAsync(new Loading());

            ResponseService<Fotografo> responseService = await _fotografoService.ObterFotografo(email, senha);


            if(responseService.IsSuccess)
            {
                App.Current.Properties.Add("Fotografo", JsonConvert.SerializeObject(responseService.Data));
                await App.Current.SavePropertiesAsync();

                App.Current.MainPage = new HomePage();
            }

            else
            {
                if(responseService.StatusCode == 404)
                {
                    await DisplayAlert("Erro!", "E-mail e/ou senha incorreto(s)!", "OK");
                }
                else
                {
                    await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais Tarde!", "OK");
                }
            }
            await Navigation.PopAllPopupAsync();
        }
    }
}