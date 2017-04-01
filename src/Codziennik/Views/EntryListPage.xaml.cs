﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codziennik.Models;
using Xamarin.Forms;
using Codziennik.Data;

namespace Codziennik.Views
{
    public partial class EntryListPage : ContentPage
    {

        public EntryListPage()
        {
            InitializeComponent();

            entryListView.IsPullToRefreshEnabled = true;
            entryListView.RefreshCommand = new Command(async () =>
            {
                await LoadData();
                entryListView.IsRefreshing = false;
            });

            var toolbarItem = new ToolbarItem
            {
                Text = "+"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new NewEntryPage());
            };
            ToolbarItems.Add(toolbarItem);

            var settingsToolbarItem = new ToolbarItem
            {
                Text = "Ustawienia"
            };
            settingsToolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new SettingsPage());
            };
            ToolbarItems.Add(settingsToolbarItem);


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

            List<Models.Entry> list = await EntryDataStorage.ReadAllEntries();

            entryListView.ItemsSource = list;

            if (list.Count == 0)
                await DisplayAlert("Brak wpisów", "Dodaj swój pierwszy wpis!", "OK");

            IsBusy = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        }
}
