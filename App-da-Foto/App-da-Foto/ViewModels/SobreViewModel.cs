using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
    public class SobreViewModel : BaseViewModel
    {
        public SobreViewModel()
        {
            Title = "Sobre";
        }

        public ICommand OpenWebCommand { get; }
    }
}