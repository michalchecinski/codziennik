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

            MainPage = new NavigationPage(new EntryListPage())
            {
                Title = "Lista wpisów",
                BarBackgroundColor = Color.FromHex("23b2dd")
            };

        }

        const string storedDataName = "storedData";

        public string StoredData { get; set; } = null;

        protected override void OnStart()
        {
                StoredData = null;
        }

        protected override void OnSleep()
        {
            Properties[storedDataName] = StoredData;
        }

        protected override void OnResume()
        {
            if (Properties.ContainsKey(storedDataName))
                StoredData = (string)Properties[storedDataName];
            else
                StoredData = null;
        }
    }
}
