using Codziennik.Data;
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


        public EditEntryPage(Models.Entry passedEntry)
        {

            entry = passedEntry;

            Title = "Edytuj wpis";

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
            foreach (Editor editor in answersEditors)
            {
                entry.Answers.Add(editor.Text);
            }
            await EntryDataStorage.WriteEditedEntryAsync(entry);
            await Navigation.PopAsync();
        }
    }
}
