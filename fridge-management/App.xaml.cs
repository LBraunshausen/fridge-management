using fridge_management.Services;
using fridge_management.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fridge_management
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
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
