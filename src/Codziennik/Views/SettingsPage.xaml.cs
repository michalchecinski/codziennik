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

            string questionsString = QuestionsEditor.Text;

            List<string> Questions = new List<string>(
                           questionsString.Split(new string[] { "\n" },
                           StringSplitOptions.RemoveEmptyEntries));

            await SettingsDataStorage.WriteAllQuestions(Questions);

        }
    }
}
