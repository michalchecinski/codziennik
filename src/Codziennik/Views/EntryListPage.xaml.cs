using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codziennik.Models;
using Xamarin.Forms;
using Codziennik.Data;
using Codziennik.RESX;

namespace Codziennik.Views
{
    public partial class EntryListPage : ContentPage
    {

        List<Models.Entry> list = null;

        public EntryListPage()
        {
            InitializeComponent();

            entryListView.IsPullToRefreshEnabled = true;
            entryListView.RefreshCommand = new Command(async () =>
            {
                await LoadData();
                entryListView.IsRefreshing = false;
            });


            var settingsToolbarItem = new ToolbarItem
            {
                Text = AppResources.Settings,
                Icon="settings.png"
            };
            settingsToolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new SettingsPage());
            };
            ToolbarItems.Add(settingsToolbarItem);

            var searchToolbarItem = new ToolbarItem
            {
                Text = AppResources.Search,
                Icon="search.png"
            };
            searchToolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new SearchDate(list));
            };
            ToolbarItems.Add(searchToolbarItem);

            var toolbarItem = new ToolbarItem
            {
                Text = AppResources.Add,
                Icon="add.png"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new NewEntryPage());
            };
            ToolbarItems.Add(toolbarItem);

            entryListView.ItemTapped += async (sender, e) =>
            {
                var item = e.Item as Models.Entry;
                if (item == null)
                    return;
                await Navigation.PushAsync(new ShowEntryPage(item));
                entryListView.SelectedItem = null;
            };
        }

        async private Task LoadData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                list = await EntryDataStorage.ReadAllEntries();
            }
            catch (Exception)
            {
                await DisplayAlert(AppResources.Error, AppResources.ReadEntriesFailed, "OK");
            }
            

            entryListView.ItemsSource = list;

            if (list.Count == 0)
                await DisplayAlert(AppResources.EntriesLack, AppResources.EntriesLackDescription, "OK");

            IsBusy = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }
    }
}
