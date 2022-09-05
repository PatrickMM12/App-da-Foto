using App_da_Foto.ViewModels;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class HomePage : Shell
    {
        public HomePage()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FotografoPerfilPage), typeof(FotografoPerfilPage));
            Routing.RegisterRoute(nameof(NovoFotografoPage), typeof(NovoFotografoPage));
            Routing.RegisterRoute(nameof(BuscarLugarPage), typeof(BuscarLugarPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            BindingContext = new HomeViewModel();

            if (App.Current.Properties.ContainsKey("Fotografo"))
            {
                CurrentItem = mapa;
            }
        }
    }
}