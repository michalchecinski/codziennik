using System;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime EntryDate { get; private set; }

        public string EntryContent { get; private set; }

        public string EntryDateString { get; private set; }

        public Entry(string entryContent, DateTime entryDate)
        {
            this.EntryDate = entryDate;
            entryDateToString();
            this.EntryContent = entryContent;
        }

        public Entry(string entryContent)
        {
            SetEntryDate();
            this.EntryContent = entryContent;
        }

        private void SetEntryDate()
        {
            EntryDate = DateTime.Now;
            entryDateToString();
        }

        private void entryDateToString()
        {
            EntryDateString = EntryDate.ToString("dd.MM.yyyy HH:mm");
        }

    }
}
