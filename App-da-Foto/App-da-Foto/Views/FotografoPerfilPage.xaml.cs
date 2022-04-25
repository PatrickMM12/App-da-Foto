using App_da_Foto.ViewModels;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
	public partial class FotografoPerfilPage : ContentPage
	{
		public FotografoPerfilPage()
		{
			InitializeComponent();
			BindingContext = new FotografoPerfilViewModel();
		}
	}
}