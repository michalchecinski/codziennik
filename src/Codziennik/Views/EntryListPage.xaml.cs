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

            List<Models.Entry> entries = new List<Models.Entry>
            {
                new Models.Entry("Pierwszy wpis"),
                new Models.Entry("Drugi wpis", new DateTime(2017, 02, 02)),
                new Models.Entry("Kolejny wpis", new DateTime(2016, 12, 28)),
                new Models.Entry("Content", new DateTime(2010, 5, 12))
            };

            for (int i = 1; i < 20; i++)
            {
                entries.Add(new Models.Entry($"{i} wpis", new DateTime(2017, 03, i)));
            }
         
            entryListView.ItemsSource = entries;
        }
    }
}
