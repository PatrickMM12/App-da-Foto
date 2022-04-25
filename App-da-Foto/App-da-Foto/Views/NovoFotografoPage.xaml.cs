using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
	public partial class NovoFotografoPage : ContentPage
	{
		public Fotografo Fotografo { get; set; }

		public NovoFotografoPage()
		{
			InitializeComponent();
			BindingContext = new NovoFotografoViewModel();
		}
	}
}