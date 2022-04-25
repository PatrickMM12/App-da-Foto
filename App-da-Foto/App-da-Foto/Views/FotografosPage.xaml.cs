using App_da_Foto.ViewModels;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
	public partial class FotografosPage : ContentPage
	{
		FotografosViewModel _viewModel;

		public FotografosPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new FotografosViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAparecendo();
		}
	}
}