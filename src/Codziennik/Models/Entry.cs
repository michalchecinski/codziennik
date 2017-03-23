using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime EntryDate { get; set; }

        public List<string> Answers { get; set; }

        public List<string> Questions { get; set; }

        [JsonIgnore]
        public string EntryDateString {
            get
            {
                return EntryDate.ToString("dd.MM.yyyy HH:mm");
            }
        }

        public Entry()
        {
            SetEntryDateNow();
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


        public override int GetHashCode()
        {
            return 17 * (EntryDate.GetHashCode() + Answers.GetHashCode());
        }

    }
}
