using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Codziennik.Models;

namespace Codziennik
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ShowCurrentDateClicked(object o, EventArgs e)
        {
            Models.Entry newEntry = new Models.Entry("");
            newEntry.SetEntryDate();


            //DisplayAlert("Current date:", $"{newEntry.entryDate.ToString()}", "OK");
        }
    }
}
