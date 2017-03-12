using System;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime entryDate { get; private set; }

        public string entryContent { get; private set; }

        public string entryDateString { get; private set; }

        public Entry(string entryContent, DateTime entryDate)
        {
            this.entryDate = entryDate;
            entryDateToString();
            this.entryContent = entryContent;
        }

        public Entry(string entryContent)
        {
            SetEntryDate();
            this.entryContent = entryContent;
        }

        private void SetEntryDate()
        {
            entryDate = DateTime.Now;
            entryDateToString();
        }

        private void entryDateToString()
        {
            entryDateString = entryDate.ToString("dd.MM.yyyy HH:mm");
        }

    }
}
