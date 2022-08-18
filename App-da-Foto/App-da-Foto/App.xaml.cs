using Repositories;
using Xamarin.Forms;

namespace App_da_Foto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<FotografoRepositorio>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
