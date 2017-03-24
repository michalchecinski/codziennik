using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Codziennik.Data;

namespace Codziennik.Views
{
    public partial class ShowEntryPage : ContentPage
    {
        public ShowEntryPage(Models.Entry entry)
        {
            InitializeComponent();

            EntryDateLabel.Text = entry.EntryDate.ToString();
            //EntryContentLabel.Text = entry.EntryContent;

            var toolbarItem = new ToolbarItem
            {
                Text = "-"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                bool deleteConfirmed = await DisplayAlert("Chcesz usunąć wpis?", "Uwaga! Ta operacja jest nieodwracalna", "Usuń", "Anuluj");
                if(deleteConfirmed == true)
                {
                    await DataStorage.DeleteEntry(entry);
                    await DisplayAlert("Usunięto wpis", entry.EntryDate.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            };
            ToolbarItems.Add(toolbarItem);
        }
    }
}
