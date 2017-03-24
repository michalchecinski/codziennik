using Codziennik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Codziennik.Views
{
    public class EntryPage : ContentPage
    {
        public EntryPage()
        {
            Title = "Dodaj nowy wpis";
            Models.Entry entry = new Models.Entry();

            var layout = new StackLayout
            {
                Children = {
                    new Label { Text = "Opowiedz o swoim dniu", FontSize = 20 }
                },
                Spacing = 10,
                Margin = new Thickness(20, 5)
               
            };

            foreach (string question in entry.Questions)
            {
                var questionLabel = new Label { Text = question, HorizontalTextAlignment = TextAlignment.Center };
                var answerEditor = new Editor { HorizontalOptions = LayoutOptions.Fill, HeightRequest = 150 };
                layout.Children.Add(questionLabel);
                layout.Children.Add(answerEditor);
            }
            var saveButton = new Button() { Text = "Zapisz", HorizontalOptions = LayoutOptions.Center};
            saveButton.Clicked += SaveButtonClicked;
            layout.Children.Add(saveButton);

            var scrollview = new ScrollView().Content = layout;

            this.Content = scrollview;
            
        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
                //await DataStorage.WriteOneEntry(null);
                await Navigation.PopAsync();
        }
    }
}
