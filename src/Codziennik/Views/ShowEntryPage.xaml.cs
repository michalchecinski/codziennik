using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Codziennik.Views
{
    public partial class ShowEntryPage : ContentPage
    {
        public ShowEntryPage(Models.Entry entry)
        {
            InitializeComponent();

            EntryDateLabel.Text = entry.EntryDateString;
            EntryContentLabel.Text = entry.EntryContent;
        }
    }
}
