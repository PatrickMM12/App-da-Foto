using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class SairViewModel : BaseViewModel
    {
        public SairViewModel()
        {
            SairCommand = new Command(OnSairClicked);
            VoltarCommand = new Command(OnVoltarClicked);
        }

        public Command SairCommand { get; }
        public Command VoltarCommand { get; }

        private async void OnSairClicked(object obj)
        {
            Analytics.TrackEvent("Logout Fotografo");
            try
            {
                App.Current.Properties.Remove("Fotografo");
                App.Current.Properties.Clear();
                await App.Current.SavePropertiesAsync();
                Logado = false;
                await Shell.Current.GoToAsync("///LoginPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro!", ex.Message.ToString(), "Ok");
            }
        }

        private void OnVoltarClicked(object obj)
        {
            Shell.Current.GoToAsync("..");
        }
    }
}
