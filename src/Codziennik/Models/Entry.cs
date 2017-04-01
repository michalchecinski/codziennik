using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Codziennik.Data;

namespace Codziennik.Models
{
    public class Entry
    {
        public DateTime Date { get; set; }

        public List<string> Answers { get; set; }

        public List<string> Questions { get; set; }

        [JsonIgnore]
        public string EntryDateString {
            get
            {
                return Date.ToString("dd.MM.yyyy HH:mm");
            }
        }

        public Entry()
        {
        }

        public void SetEntryDateNow()
        {
            Date = DateTime.Now;
        }

        public void SetQuestions()
        {
            //Questions = await SettingsDataStorage.ReadAllQuestions();

            if (Questions == null || Questions.Count == 0)
            {
                this.Questions = new List<string>
                {
                    "Za co jestem wdzięczny?",
                    "Wczorajsze zwycięstwo"
                };
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Entry objAsEntry = obj as Entry;
            if (objAsEntry == null)
                return false;
            else
                return (this.Date.Equals(objAsEntry.Date));
        }

        private bool EqualsTwoLists(List<string> first, List<string> second)
        {
            if (first == null || second == null)
                return false;
            if (first.Count != second.Count)
                return false;

            var firstArray = first.ToArray();
            var secondArray = second.ToArray();

            for (int i = 0; i < first.Count; i++)
            {
                if (firstArray[i] != secondArray[i])
                    return false;
            }

            return true;
        }


        public override int GetHashCode()
        {
            return 17 * (Date.GetHashCode() + Answers.GetHashCode());
        }

    }
}
