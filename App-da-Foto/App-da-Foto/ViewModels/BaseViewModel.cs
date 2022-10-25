using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        FotografoService fotografoService = new FotografoService();
        public FotografoService FotografoService 
        { 
            get => fotografoService; 
            set => fotografoService = value; 
        }

        EnderecoService enderecoService = new EnderecoService();
        public EnderecoService EnderecoService
        {
            get => enderecoService;
            set => enderecoService = value;
        }

        ContatoService contatoService = new ContatoService();
        public ContatoService ContatoService
        {
            get => contatoService;
            set => contatoService = value;
        }

        FotoService fotoService = new FotoService();
        public FotoService FotoService
        {
            get => fotoService;
            set => fotoService = value;
        }

        private Stream iconeLogoStream;
        public Stream IconeLogoStream
        {
            get => iconeLogoStream;
            set => iconeLogoStream = value;
        }

        private ObservableCollection<string> _listaEspecialidade = new ObservableCollection<string>
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

        private bool logado;
        public bool Logado
        {
            get => logado;
            set => SetProperty(ref logado, value);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool noResult = false;
        public bool NoResult
        {
            get { return noResult; }
            set { SetProperty(ref noResult, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
