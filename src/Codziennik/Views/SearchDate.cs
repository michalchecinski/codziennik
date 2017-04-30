using System;
using System.Collections.Generic;
using Codziennik.Models;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Codziennik.RESX;

namespace Codziennik.Views
{
    public class SearchDate : ContentPage
    {
        public SearchDate(List<Models.Entry> entryList)
        {
            var layout = new StackLayout();

            Title = AppResources.SearchEntryTitle;

            var datePicker = new DatePicker()
            {
                Date = DateTime.Now,
                HorizontalOptions = LayoutOptions.Center
            };

            layout.Children.Add(datePicker);

            List<Models.Entry> searchList = null;

            datePicker.DateSelected += (object sender, DateChangedEventArgs e) =>
            {
                if (layout.Children.Count > 2)
                        layout.Children.RemoveAt(2);                    
                if(layout.Children.Count > 1)
                    layout.Children.RemoveAt(1);

                searchList = entryList.FindAll( x => x.Date.Date.Equals(datePicker.Date.Date) );

                int count = searchList.Count;

                if ( count != 0)
                {
                    var label = new Label()
                    {
                        Text = AppResources.Found + " " + count + " " + AppResources.EntriesWithDate,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Black,
                        Margin = new Thickness(10, 10)
                    };
                    layout.Children.Add(label);
                    layout.Children.Add(CreateListview(searchList));
                }
                else
                {
                    var label = new Label()
                    {
                        Text = AppResources.NoneEntriesWithDate,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Black,
                        Margin = new Thickness(10, 10)
                    };

                    layout.Children.Add(label);
                }
            };

            this.Content = layout;

            
        }

        ListView CreateListview(List<Models.Entry> list)
        {
            var listView = new ListView()
            {
                ItemsSource = list,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                SeparatorColor = Color.Black
            };

            listView.ItemTemplate = new DataTemplate(() =>
            {
                Label dateLabel = new Label()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    TextColor = Color.Black
                };

                dateLabel.SetBinding(Label.TextProperty, "EntryDateString");

                return new ViewCell
                {
                    View = new StackLayout()
                    {
                        Children = { dateLabel }
                    }
                };
            });

            listView.ItemTapped += async (sender, e) =>
            {
                var item = e.Item as Models.Entry;
                if (item == null)
                    return;
                await Navigation.PushAsync(new ShowEntryPage(item));
                listView.SelectedItem = null;
            };

            return listView;
        }



    }
}
