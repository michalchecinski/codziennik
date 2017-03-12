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

            Assert.Equal(testEntry.entryContent, "Testowy wpis");
            Assert.Equal(testEntry.entryDateString, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        }

        [Fact]
        public void Adding_entry_date_and_content_with_ctor()
        {
            Models.Entry testEntry = new Models.Entry("Test", new DateTime(2017, 01, 01));

            Assert.Equal(testEntry.entryDate, new DateTime(2017, 01, 01));
            Assert.Equal(testEntry.entryContent, "Test");
        }

        [Fact]
        public void Entry_Date_To_String()
        {
            Models.Entry testEntry = new Models.Entry("Test", new DateTime(2017, 02, 01, 22, 11, 00));

            Assert.Equal(testEntry.entryDateString, "01.02.2017 22:11");
            Assert.Equal(testEntry.entryContent, "Test");
        }
        
    }
}
