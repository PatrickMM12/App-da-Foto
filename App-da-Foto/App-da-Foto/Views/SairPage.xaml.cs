using App_da_Foto.ViewModels;
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
    public partial class SairPage : ContentPage
    {
        public SairPage()
        {
            InitializeComponent();
        }

        private async void Sair(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("Fotografo");
            await App.Current.SavePropertiesAsync();
            await Shell.Current.GoToAsync("///LoginPage");
        }

        private void Voltar(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//HomePage/MapaPage");
        }
    }
}