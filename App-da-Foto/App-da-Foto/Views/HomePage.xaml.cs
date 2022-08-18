using App_da_Foto.ViewModels;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Map.GetActualLocationCommand.Execute(null);
        }
    }
}