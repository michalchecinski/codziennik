using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime EntryDate { get; set; }

        public List<string> Answers { get; set; }

        public List<string> Questions { get; private set; }

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
            SetQuestions();
        }

        private void SetEntryDateNow()
        {
            EntryDate = DateTime.Now;
        }

        private void SetQuestions()
        {
            this.Questions = new List<string>();
            this.Questions.Add("Za co jestem wdzięczny?");
            this.Questions.Add("Wczorajsze zwycięstwo");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Entry objAsEntry = obj as Entry;
            if (objAsEntry == null)
                return false;
            else
                return (this.EntryDate.Equals(objAsEntry.EntryDate) && this.Answers.Equals(objAsEntry.Answers));
        }


        public override int GetHashCode()
        {
            return 17 * (EntryDate.GetHashCode() + Answers.GetHashCode());
        }

    }
}
