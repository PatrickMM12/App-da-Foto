using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IFotografoRepositorio<Fotografo> DataStore => DependencyService.Get<IFotografoRepositorio<Fotografo>>();
        protected FotografoService fotografoService;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        internal FotografoService FotografoService 
        { 
            get => fotografoService; 
            set => fotografoService = value; 
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
