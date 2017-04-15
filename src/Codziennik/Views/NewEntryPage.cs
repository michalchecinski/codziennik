using Codziennik.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Codziennik.Views
{
    public class NewEntryPage : ContentPage
    {
        List<Editor> answersEditors = new List<Editor>();
        Models.Entry entry = null;

        App app = App.Current as App;
        bool propertiesTaken = false;

        

        public NewEntryPage()
        {

            
            Title = "Dodaj nowy wpis";

            var accepptToolbarItem = new ToolbarItem
            {
                Text = "Zapisz"
            };
            accepptToolbarItem.Clicked += SaveButtonClicked;
            ToolbarItems.Add(accepptToolbarItem);
        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            app.StoredData = null;
            entry.Answers = new List<string>();
            foreach(Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }
            entry.SetEntryDateNow();
            await EntryDataStorage.WriteOneEntryAsync(entry);
            await Navigation.PopAsync();
        }

        void SaveProperties(object sender, EventArgs e)
        {
            entry.Answers = new List<string>();
            foreach (Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }

            entry.SetEntryDateNow();

            app.StoredData = JsonConvert.SerializeObject(entry);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (app.StoredData != null)
            {
                entry = JsonConvert.DeserializeObject<Models.Entry>(app.StoredData);
                propertiesTaken = true;
                app.StoredData = null;
            }
            else
            {
                entry = new Models.Entry();
                await entry.SetQuestions();
            }

            var layout = new StackLayout
            {
                Children = {
                    new Label { Text = "Opowiedz o swoim dniu", FontSize = 20 }
                },
                Spacing = 10,
                Margin = new Thickness(20, 5)
            };

            int i = 0;

            foreach (string question in entry.Questions)
            {
                var questionLabel = new Label { Text = question, HorizontalTextAlignment = TextAlignment.Center };
                var answerEditor = new Editor { HorizontalOptions = LayoutOptions.Fill, HeightRequest = 150 };
                answerEditor.TextChanged += SaveProperties;
                if (propertiesTaken)
                {
                    answerEditor.Text = entry.Answers[i];
                    i++;
                }                   
                answersEditors.Add(answerEditor);
                layout.Children.Add(questionLabel);
                layout.Children.Add(answerEditor);
            }
            var saveButton = new Button() { Text = "Zapisz", HorizontalOptions = LayoutOptions.Center };
            saveButton.Clicked += SaveButtonClicked;
            layout.Children.Add(saveButton);

            var scrollview = new ScrollView()
            {
                Content = layout
            };

            this.Content = scrollview;

        }

        

    }
}
