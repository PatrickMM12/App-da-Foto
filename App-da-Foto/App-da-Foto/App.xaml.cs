﻿using App_da_Foto.Views;
using Repositories;
using System;
using Xamarin.Forms;

namespace App_da_Foto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
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
