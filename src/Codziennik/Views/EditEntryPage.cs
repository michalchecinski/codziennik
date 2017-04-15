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
        public Models.Entry entry { get; private set; } = null;
        bool saved = false;

        App app = App.Current as App;


        public EditEntryPage(Models.Entry passedEntry)
        {       

            if(app.StoredData == null)
            {
                entry = passedEntry;
            }

            Title = "Edytuj wpis";


            var accepptToolbarItem = new ToolbarItem
            {
                Text = "Zapisz"                
            };
            accepptToolbarItem.Clicked += SaveButtonClicked;
            ToolbarItems.Add(accepptToolbarItem);

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
                    new Label { Text = entry.EntryDateString, FontSize = 20 }
                },
                Spacing = 10,
                Margin = new Thickness(20, 5)
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


            var saveButton = new Button() { Text = "Zapisz", HorizontalOptions = LayoutOptions.Center };
            saveButton.Clicked += SaveButtonClicked;
            layout.Children.Add(saveButton);

            var scrollview = new ScrollView()
            {
                Content = layout
            };

            this.Content = scrollview;
        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            app.StoredData = null;
            saved = true;
            entry.Answers = new List<string>();
            foreach (Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }
            await EntryDataStorage.WriteEditedEntryAsync(entry);
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

    }
}
