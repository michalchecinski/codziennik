using Codziennik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Codziennik.RESX;

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

            List<string> Questions = null;

            try
            {
                Questions = await SettingsDataStorage.ReadAllQuestions();
            }
            catch (Exception)
            {
                await DisplayAlert(AppResources.Error, AppResources.ReadQuestionsError, "OK");
            }
            

            if (Questions == null || Questions.Count == 0)
            {
                Questions = new List<string>(
                               AppResources.DefaultQuestions.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            }

            QuestionsEditor.Text = String.Join("\n", Questions);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            List<string> Questions = null;

            if (QuestionsEditor.Text == String.Empty || QuestionsEditor.Text == null)
            {

                await DisplayAlert(AppResources.QuestionLack, AppResources.QuestionsLackDescription, "OK");

                Questions = new List<string>(
                               AppResources.DefaultQuestions.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                string questionsString = QuestionsEditor.Text;

                Questions = new List<string>(
                               questionsString.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            }


            try
            {
                await SettingsDataStorage.WriteAllQuestions(Questions);
            }
            catch (Exception)
            {
                await DisplayAlert(AppResources.Error, AppResources.SaveQuestionsError, "OK");
            }
            

        }

        private void RestoreDefaultQuestions(object sender, EventArgs e)
        {
            List<string> Questions = new List<string>(
                               AppResources.DefaultQuestions.Split(new string[] { "\n" },
                               StringSplitOptions.RemoveEmptyEntries));

            QuestionsEditor.Text = AppResources.DefaultQuestions;
        }
    }
}
