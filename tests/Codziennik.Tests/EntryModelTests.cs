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
        public void Adding_Entry_Content_with_Ctor_test()
        {
            Models.Entry testEntry = new Models.Entry("Testowy wpis");

            Assert.Equal(testEntry.EntryContent, "Testowy wpis");
            Assert.Equal(testEntry.EntryDateString, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        }
        
    }
}
