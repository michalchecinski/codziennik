using Newtonsoft.Json;
using System;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime EntryDate { get; set; }

        public string EntryContent { get; set; }

        [JsonIgnore]
        public string EntryDateString {
            get
            {
                return EntryDate.ToString("dd.MM.yyyy HH:mm");
            }
        }

        public Entry(string entryContent)
        {
            SetEntryDateNow();
            this.EntryContent = entryContent;
        }

        public Entry()
        {
        }

        private void SetEntryDateNow()
        {
            EntryDate = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Entry objAsEntry = obj as Entry;
            if (objAsEntry == null)
                return false;
            else
                return (this.EntryDate.Equals(objAsEntry.EntryDate) && this.EntryContent.Equals(objAsEntry.EntryContent));
        }
    }
}
