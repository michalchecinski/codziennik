using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Codziennik.Tests
{
    public class EntryModelTests
    {
        [Fact]
        public void Adding_Entry_Content_and_no_date_with_Ctor_test()
        {
            Models.Entry testEntry = new Models.Entry("Testowy wpis");

            Assert.Equal(testEntry.EntryContent, "Testowy wpis");
            Assert.Equal(testEntry.EntryDateString, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        }

        [Fact]
        public void Adding_entry_date_and_content_with_ctor()
        {
            Models.Entry testEntry = new Models.Entry("Test", new DateTime(2017, 01, 01));

            Assert.Equal(testEntry.EntryDate, new DateTime(2017, 01, 01));
            Assert.Equal(testEntry.EntryContent, "Test");
        }

        [Fact]
        public void Entry_Date_To_String()
        {
            Models.Entry testEntry = new Models.Entry("Test", new DateTime(2017, 02, 01, 22, 11, 00));

            Assert.Equal(testEntry.EntryDateString, "01.02.2017 22:11");
            Assert.Equal(testEntry.EntryContent, "Test");
        }
        
    }
}
