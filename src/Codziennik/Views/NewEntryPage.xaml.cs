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
            string EditorText = WhatHappenedEditor.Text;
            if(EditorText != null)
                DisplayAlert("Zapisz", "Naciśnięto przycisk zapisywania", "OK");
            else
                DisplayAlert("Błąd!", "Wprowadź tekst do zapisania", "OK");
        }
    }
}
