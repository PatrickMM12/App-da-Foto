using App_da_Foto.Views;
using System;
using Xamarin.Forms;

namespace App_da_Foto
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(FotografoPerfilPage), typeof(FotografoPerfilPage));
			Routing.RegisterRoute(nameof(NovoFotografoPage), typeof(NovoFotografoPage));
		}

		private async void OnMenuItemClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//LoginPage");
		}
	}
}
