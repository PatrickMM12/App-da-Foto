using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class SobreViewModel : BaseViewModel
    {

        public Command OpenWebCommand { get; set; }
        public SobreViewModel()
        {
            OpenWebCommand = new Command(WebCommand);
        }

        private async void WebCommand()
        {
            Uri uri = new Uri("https://github.com/PatrickMM12/App-da-Foto");
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}