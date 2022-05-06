using App_da_Foto.Views;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command AddFotografoCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            AddFotografoCommand = new Command(OnAdicionarFotografo);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnAdicionarFotografo(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NovoFotografoPage));
        }
    }
}
