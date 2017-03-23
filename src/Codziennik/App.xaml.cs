using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using Codziennik.Views;
using System.Threading.Tasks;

namespace Codziennik
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new EntryListPage())
            //{
            //    Title = "Lista wpisów",
            //    BarBackgroundColor = Color.FromHex("23b2dd")
            //};

            MainPage = new EntryPage();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
