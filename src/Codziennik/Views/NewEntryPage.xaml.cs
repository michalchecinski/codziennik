using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Codziennik.Views
{
    public partial class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            InitializeComponent();
        }

        void SaveButtonClicked(object sender, EventArgs e)
        {
            if(WhatHappenedEditor.Text != null)
            {
                Models.Entry newEntry = new Models.Entry(WhatHappenedEditor.Text, DateTime.Now);
                DisplayAlert("Zapisano", "", "OK");
                
            }
            else
                DisplayAlert("Błąd!", "Wprowadź tekst do zapisania", "OK");
        }
    }
}
