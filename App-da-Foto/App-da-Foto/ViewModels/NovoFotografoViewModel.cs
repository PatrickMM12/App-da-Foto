using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.Views;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class NovoFotografoViewModel : BaseViewModel
    {
        private string nome;
        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string especialidade;
        public string Especialidade
        {
            get => especialidade;
            set => SetProperty(ref especialidade, value);
        }

        private string senha;
        public string Text
        {
            get => senha;
            set => SetProperty(ref senha, value);
        }

        private string message;
        public string Message 
        { 
            get => message; 
            set => SetProperty(ref message, value); 
        }

        public NovoFotografoViewModel()
        {
            FotografoService = new FotografoService();

            SalvarFotografoCommand = new Command(OnSalvarFotografo);
            CancelarCommand = new Command(OnCancelar);
            PropertyChanged +=
                (_, __) => SalvarFotografoCommand.ChangeCanExecute();
        }

        public Command SalvarFotografoCommand { get; }
        public Command CancelarCommand { get; }

        private async void OnSalvarFotografo()
        {
            if (ValidarSalvar())
            {
                await Shell.Current.Navigation.PushPopupAsync(new Loading());
                Analytics.TrackEvent("Cadastrar Fotografo");
                Message = String.Empty;

                Fotografo newFotografo = new Fotografo()
                {
                    Nome = Nome = Regex.Replace(nome, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                    Especialidade = Especialidade,
                    Email = Email.ToLower(),
                    Senha = Text,
                };

                try
                {
                    ResponseService<Fotografo> responseService = await FotografoService.AdicionarFotografo(newFotografo);

                    if (responseService.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Sucesso!", "Cadastrado realizado com Sucesso! Por favor realizar Login", "Ok");
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        Message = responseService.Errors.ToString();
                        await Shell.Current.DisplayAlert("Erro!", Message, "Ok");
                    }
                }
                catch
                {
                    Message = "E-mail já cadastrado!";
                }
                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ops!", "Preencha todos os campos!", "Ok");
            }
        }

        private bool ValidarSalvar()
        {
            return !String.IsNullOrWhiteSpace(nome)
                && !String.IsNullOrWhiteSpace(especialidade)
                && !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(senha);
        }

        private async void OnCancelar()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

