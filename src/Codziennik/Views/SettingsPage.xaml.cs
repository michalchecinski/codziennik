using Codziennik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Codziennik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<string> Questions = await SettingsDataStorage.ReadAllQuestions();

            if (Questions == null || Questions.Count == 0)
            {
                Questions = new List<string>
                {
                    "Za co jestem wdzięczny?",
                    "Wczorajsze zwycięstwo"
                };
            }

            QuestionsEditor.Text = String.Join("\n", Questions);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            List<string> Questions = null;

            if (QuestionsEditor.Text == String.Empty || QuestionsEditor.Text == null)
            {

                await DisplayAlert("Brak pytań!", "Pytania zostały ustawione na domyślne", "OK");

                Questions = new List<string>
                {
                    "Za co jestem wdzięczny?",
                    "Wczorajsze zwycięstwo"
                };
            }
            else
            {
                string questionsString = QuestionsEditor.Text;

                Questions = new List<string>(
                               questionsString.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            }
                
            

            await SettingsDataStorage.WriteAllQuestions(Questions);

        }

        private void RestoreDefaultQuestions(object sender, EventArgs e)
        {
            List<string> Questions = new List<string>
                {
                    "Za co jestem wdzięczny?",
                    "Wczorajsze zwycięstwo"
                };

            QuestionsEditor.Text = String.Join("\n", Questions);
        }
    }
}
