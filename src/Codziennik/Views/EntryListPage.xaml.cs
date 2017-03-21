using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codziennik.Models;
using Xamarin.Forms;

namespace Codziennik.Views
{
    public partial class EntryListPage : ContentPage
    {
        public EntryListPage()
        {
            InitializeComponent();

            var toolbarItem = new ToolbarItem
            {
                Text = "+"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new NewEntryPage());
            };
            ToolbarItems.Add(toolbarItem);

            List<Models.Entry> entries = new List<Models.Entry>
            {
                new Models.Entry("Pierwszy wpis"),
                new Models.Entry("Drugi wpis", new DateTime(2017, 02, 02)),
                new Models.Entry("Kolejny wpis", new DateTime(2016, 12, 28)),
                new Models.Entry("Content", new DateTime(2010, 5, 12))
            };
         
            entryListView.ItemsSource = entries;

            entryListView.ItemTapped += async (sender, e) =>
            {
                var item = e.Item as Models.Entry;
                if (item == null)
                    return;
                await Navigation.PushAsync(new ShowEntryPage(item));
                entryListView.SelectedItem = null;
            };
        }
    }
}
