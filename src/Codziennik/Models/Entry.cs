using System;

namespace Codziennik.Models
{
    class Entry
    {
        public DateTime entryDate { get; set; }

        public string entryContent { get; set; }

        public Entry(string entryContent, DateTime entrytDate)
        {
            this.entryDate = entryDate;
            this.entryContent = entryContent;
        }

        public Entry(string entryContent)
        {
            SetEntryDate();
            this.entryContent = entryContent;
        }

        public void SetEntryDate()
        {
            entryDate = DateTime.Now;
        }

    }
}
