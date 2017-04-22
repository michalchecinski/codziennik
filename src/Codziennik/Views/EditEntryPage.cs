using Codziennik.Data;
using Codziennik.RESX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Codziennik.Views
{
    public class EditEntryPage : ContentPage
    {
        List<Editor> answersEditors = new List<Editor>();
        public Models.Entry entry { get; private set; } = null;

        App app = App.Current as App;


        public EditEntryPage(Models.Entry passedEntry)
        {       
            if(app.StoredData == null)
            {
                entry = passedEntry;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (app.StoredData != null)
            {
                entry = JsonConvert.DeserializeObject<Models.Entry>(app.StoredData);
                app.StoredData = null;
            }


            var layout = new StackLayout
            {
                Children = {
                    new Label { Text = AppResources.EditEntry + " " + entry.EntryDateString, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center }
                },
                Spacing = 10,
                Margin = new Thickness(20, 20)
            };



            var questionsAndAnswers = entry.Questions.Zip(entry.Answers, (q, a) => new { Question = q, Answer = a });
            foreach (var qa in questionsAndAnswers)
            {
                var questionLabel = new Label { Text = qa.Question, HorizontalTextAlignment = TextAlignment.Center };
                var answerEditor = new Editor { Text = qa.Answer, HorizontalOptions = LayoutOptions.Fill, HeightRequest = 150 };
                answerEditor.TextChanged += SaveProperties;
                answersEditors.Add(answerEditor);
                layout.Children.Add(questionLabel);
                layout.Children.Add(answerEditor);
            }


            var saveButton = new Button() { Text = AppResources.Save };
            saveButton.Clicked += SaveButtonClicked;
            var cancelButton = new Button() { Text = AppResources.Cancel };
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
            if (await DisplayAlert(AppResources.ExitQuestionTitle, AppResources.ExitQuestionDescription, AppResources.ExitQuestionExit, AppResources.ExitQuestionStay))
            {
                app.StoredData = null;
                await Navigation.PopModalAsync();
            }

        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            app.StoredData = null;
            entry.Answers = new List<string>();
            foreach (Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }

            try
            {
                await EntryDataStorage.WriteEditedEntryAsync(entry);
            }
            catch(IOException ex)
            {
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessageSaveFile, "OK");
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

            app.StoredData = JsonConvert.SerializeObject(entry);
        }

        protected override bool OnBackButtonPressed()
        {

            DisplayAlert(AppResources.SaveOrDeleteTitle, AppResources.SaveOrDeleteDescription, "OK");
            return true;
        }

    }
}
