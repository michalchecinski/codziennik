using Codziennik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Codziennik.Views
{
    public class ShowEntryPage : ContentPage
    {

        bool edited = false;
        Models.Entry passedEntry = null;
        public ShowEntryPage(Models.Entry entry)
        {
            passedEntry = entry;
            var toolbarItemDelete = new ToolbarItem
            {
                Text = "Usuń"
            };
            toolbarItemDelete.Clicked += async (sender, e) =>
            {
                bool deleteConfirmed = await DisplayAlert("Chcesz usunąć wpis?", "Uwaga! Ta operacja jest nieodwracalna", "Usuń", "Anuluj");
                if (deleteConfirmed == true)
                {
                    await EntryDataStorage.DeleteEntryAsync(entry);
                    await DisplayAlert("Usunięto wpis", entry.EntryDateString, "OK");
                    await Navigation.PopAsync();
                }
            };
            ToolbarItems.Add(toolbarItemDelete);

            var toolbarItemEdit = new ToolbarItem
            {
                Text = "Edytuj"
            };
            toolbarItemEdit.Clicked += async (sender, e) =>
            {
                edited = true;
                await Navigation.PushAsync(new EditEntryPage(entry));
            };
            ToolbarItems.Add(toolbarItemEdit);

            MakeLayoutAndLoadData(entry);

        }

        private void MakeLayoutAndLoadData(Models.Entry entry)
        {
            var layout = new StackLayout()
            {
                Spacing = 10,
                Margin = new Thickness(20, 5)
            };

            Title = entry.EntryDateString;

            var questionsAndAnswers = entry.Questions.Zip(entry.Answers, (q, a) => new { Question = q, Answer = a });
            foreach (var qa in questionsAndAnswers)
            {
                Label question = new Label() { Text = qa.Question, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.FillAndExpand };
                Label answer = new Label() { Text = qa.Answer, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.FillAndExpand };
                layout.Children.Add(question);
                layout.Children.Add(answer);
            }

            var scrollview = new ScrollView()
            {
                Content = layout
            };

            if (this.Content != null)
                this.Content = null;

            this.Content = scrollview;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(edited)
            {
                var ReadedEntry = EntryDataStorage.GetOneEntry(passedEntry);
                MakeLayoutAndLoadData(ReadedEntry);
            }
        }
    }
}
