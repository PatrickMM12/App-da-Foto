using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App_da_Foto.ViewModels
{
    public class NovoFotografoViewModel : BaseViewModel
    {
        private string nome;
        private string especialidade;
        private string email;
        private string senha;
        private string message;

        ObservableCollection<string> _listaEspecialidade;

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

        public string Message 
        { 
            get => message; 
            set => SetProperty(ref message, value); 
        }

        public ObservableCollection<string> ListaEspecialidade
        {
            get
            {
                return _listaEspecialidade;
            }
            set
            {
                _listaEspecialidade = value;
                OnPropertyChanged();
            }
        }

        public NovoFotografoViewModel()
        {
            FotografoService = new FotografoService();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            ListaEspecialidade = new ObservableCollection<string>
            {
                "Geral",
                "Retrato",
                "Casamentos",
                "Gestante",
                "New Born",
                "Infantil",
                "Corporativo",
                "Produto",
                "Preto e Branco",
                "Publicitária",
                "Moda",
                "Macrofotografia",
                "Microfotografia",
                "Aérea",
                "Artística",
                "Fotojornalismo",
                "Documental",
                "Selvagem",
                "Esportiva",
                "Viagens",
                "Subaquática",
                "Erótica",
                "Astronômica",
                "Arquitetônica",
                "Culinária",
                "Paisagem",
                "Científica"
            };
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnSave()
        {
            Message = String.Empty;

            Fotografo newFotografo = new Fotografo()
            {
                Nome = Nome,
                Especialidade = Especialidade,
                Email = Email,
                Senha = Senha,
            };

            await Shell.Current.Navigation.PushPopupAsync(new Loading());

            ResponseService<Fotografo> responseService = await FotografoService.AdicionarFotografo(newFotografo);

            if (responseService.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Sucesso!", "Cadastrado realizado com Sucesso! Por favor realizar Login", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                if (responseService.StatusCode == 400)
                {
                    StringBuilder stringBuider = new StringBuilder();
                    foreach (var dicKey in responseService.Errors)
                    {
                        foreach (var message in dicKey.Value)
                        {
                            stringBuider.AppendLine(message);
                        }
                    }
                    Message = stringBuider.ToString();
                }
            }
            await Shell.Current.Navigation.PopAllPopupAsync();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(nome)
                && !String.IsNullOrWhiteSpace(especialidade)
                && !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(senha);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

