using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    internal class MenuViewModel : BaseViewModel
    {
        public ICommand SobreCommand { get; }
        public ICommand SairCommand { get; }

        public MenuViewModel()
        {
            SobreCommand = new Command(OnSobreClicked);
            SairCommand = new Command(OnSairClicked);
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
