﻿using Codziennik.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
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
            bool propertiesTaken = false;
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
            try
            {
                await EntryDataStorage.WriteOneEntryAsync(entry);
            }
            catch (Exception)
            {
                await DisplayAlert("Błąd", "Nie udało się zapisać wpisu. Skontaktuj się z twórcą aplikacji", "OK");
            }
            
            await Navigation.PopModalAsync();
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
                    new Label { Text = "Opowiedz o swoim dniu:", FontSize = 20, HorizontalTextAlignment = TextAlignment.Center }
                },
                Spacing = 10,
                Margin = new Thickness(20, 20)
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
            var saveButton = new Button() { Text = "Zapisz" };
            saveButton.Clicked += SaveButtonClicked;
            var cancelButton = new Button() { Text = "Anuluj" };
            cancelButton.Clicked += CancelButtonClickedAsync;
            var horizontalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center
            };

            
            horizontalLayout.Children.Add(saveButton);
            horizontalLayout.Children.Add(cancelButton);

            layout.Children.Add(horizontalLayout);

            var scrollview = new ScrollView()
            {
                Content = layout
            };

            this.Content = scrollview;

        }

        private async void CancelButtonClickedAsync(object sender, EventArgs e)
        {
            if (await DisplayAlert("Wyjść bez zapisywania?", "Jesteś pewnien, że chesz wyjść bez zapisywania?", "Wyjdź", "Zostań"))
            {
                app.StoredData = null;
                await Navigation.PopModalAsync();
            }
                

        }

        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Zapisz lub odrzuć", "Zapisz lub odrzuć zaminy klikając przycisk na końcu listy pytań", "OK");
            return true;
        }



    }
}
