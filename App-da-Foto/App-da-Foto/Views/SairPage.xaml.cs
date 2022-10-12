using App_da_Foto.ViewModels;
using Microsoft.AppCenter.Analytics;
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
            BindingContext = new SairViewModel();
        }
    }
}