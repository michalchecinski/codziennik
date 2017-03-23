using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codziennik.Data;

using Xamarin.Forms;

namespace Codziennik.Views
{
    public partial class NewEntryPage : ContentPage
    {

        public NewEntryPage()
        {
            InitializeComponent();
            WhatHappenedEditor.Focused += (sender, e) => WhatHappenedEditor.Text = null;

            WhatHappenedEditor.Unfocused += (sender, e) =>
            {
                if (WhatHappenedEditor.Text == null || WhatHappenedEditor.Text == "")
                    WhatHappenedEditor.Text = "Dzisiaj zdarzyło się...";
            };

        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            if (WhatHappenedEditor.Text != null && WhatHappenedEditor.Text != "")
            {
                Models.Entry newEntry = new Models.Entry(WhatHappenedEditor.Text);
                await DataStorage.WriteOneEntry(newEntry);
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Błąd!", "Wprowadź tekst do zapisania", "OK");
        }

    }
}
