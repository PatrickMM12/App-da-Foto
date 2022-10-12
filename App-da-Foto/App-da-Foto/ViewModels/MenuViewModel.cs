using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    internal class MenuViewModel : BaseViewModel
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

        Fotografo fotografo;
        public Fotografo Fotografo 
        { 
            get => fotografo; 
            set => fotografo = value; 
        }

        public ICommand EditarPerfilCommand { get; }
        public ICommand SobreCommand { get; }
        public ICommand SairCommand { get; }

        public MenuViewModel()
        {
            Fotografo = new Fotografo();

            EditarPerfilCommand = new Command(OnEditarPerfilClicked);
            SobreCommand = new Command(OnSobreClicked);
            SairCommand = new Command(OnSairClicked);

            
        }

        public void OnAparecendo()
        {
            try
            {
                Fotografo fotografo = App.Current.Properties["Fotografo"] != null ? JsonConvert.DeserializeObject<Fotografo>(App.Current.Properties["Fotografo"].ToString()) : null;

                if (fotografo != null)
                {
                    Logado = true;
                    Nome = fotografo.Nome;
                    Especialidade = fotografo.Especialidade;
                }

            }
            catch
            {
                return;
            }
        }

        private async void OnEditarPerfilClicked(object obj)
        {
            await Shell.Current.GoToAsync("EditarPerfil");
        }

        private async void OnSobreClicked(object obj)
        {
            await Shell.Current.GoToAsync("SobrePage", true);
        }

        private async void OnSairClicked(object obj)
        {
            await Shell.Current.GoToAsync("SairPage", true);
        }
    }
}
