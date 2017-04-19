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
    public class EditEntryPage : ContentPage
    {
        List<Editor> answersEditors = new List<Editor>();
        public Models.Entry Entry { get; private set; } = null;

        App app = App.Current as App;


        public EditEntryPage(Models.Entry passedEntry)
        {       

            if(app.StoredData == null)
            {
                Entry = passedEntry;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (app.StoredData != null)
            {
                Entry = JsonConvert.DeserializeObject<Models.Entry>(app.StoredData);
                app.StoredData = null;
            }


            var layout = new StackLayout
            {
                Children = {
                    new Label { Text ="Edytuj wpis: "+ entry.EntryDateString, FontSize = 20 }
                },
                Spacing = 10,
                Margin = new Thickness(20, 5)
            };



            var questionsAndAnswers = Entry.Questions.Zip(Entry.Answers, (q, a) => new { Question = q, Answer = a });
            foreach (var qa in questionsAndAnswers)
            {
                var questionLabel = new Label { Text = qa.Question, HorizontalTextAlignment = TextAlignment.Center };
                var answerEditor = new Editor { Text = qa.Answer, HorizontalOptions = LayoutOptions.Fill, HeightRequest = 150 };
                answerEditor.TextChanged += SaveProperties;
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
            if (await DisplayAlert("Chcesz wyjść bez zapisywania?", "Jesteś pewnien, że chesz wyjść bez zapisywania?", "Wyjdź", "Zostań"))
                await Navigation.PopModalAsync();

        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            app.StoredData = null;
            Entry.Answers = new List<string>();
            foreach (Editor editor in answersEditors)
            {
                Entry.Answers.Add(editor.Text);
            }

            try
            {
                await EntryDataStorage.WriteEditedEntryAsync(Entry);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Błąd", "Nie udało się zapisać wpisu. Skontaktuj się z twórcą aplikacji", "OK");
            }

            await Navigation.PopModalAsync();
        }

        void SaveProperties(object sender, EventArgs e)
        {
            Entry.Answers = new List<string>();
            foreach (Editor editor in answersEditors)
            {
                Entry.Answers.Add(editor.Text);
            }

            Entry.SetEntryDateNow();

            app.StoredData = JsonConvert.SerializeObject(Entry);
        }

        protected override bool OnBackButtonPressed()
        {
            if (DisplayAlert("Chcesz wyjść bez zapisywania?", "Jesteś pewnien, że chesz wyjść bez zapisywania?", "Wyjdź", "Zostań").Result == true)
                return true;
            else
                return false;
        }

    }
}
