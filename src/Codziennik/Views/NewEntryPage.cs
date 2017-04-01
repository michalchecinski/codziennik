using Codziennik.Data;
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
        Models.Entry entry = new Models.Entry();

        public NewEntryPage()
        {
            Title = "Dodaj nowy wpis";

            var accepptToolbarItem = new ToolbarItem
            {
                Text = "Zapisz"
            };
            accepptToolbarItem.Clicked += (sender, e) =>
            {
                SaveButtonClicked(sender, e);
            };
            ToolbarItems.Add(accepptToolbarItem);
        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            entry.Answers = new List<string>();
            foreach(Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }
            entry.SetEntryDateNow();
            await EntryDataStorage.WriteOneEntryAsync(entry);
            await Navigation.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            var layout = new StackLayout
            {
                Children = {
                    new Label { Text = "Opowiedz o swoim dniu", FontSize = 20 }
                },
                Spacing = 10,
                Margin = new Thickness(20, 5)
            };

            await entry.SetQuestions();

            foreach (string question in entry.Questions)
            {
                var questionLabel = new Label { Text = question, HorizontalTextAlignment = TextAlignment.Center };
                var answerEditor = new Editor { HorizontalOptions = LayoutOptions.Fill, HeightRequest = 150 };
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
