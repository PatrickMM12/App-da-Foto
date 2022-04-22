using App_da_Foto.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}