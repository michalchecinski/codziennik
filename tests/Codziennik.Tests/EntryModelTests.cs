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
        public void Adding_Entry_Date_with_ctor()
        {
            Models.Entry testEntry = new Models.Entry();

            Assert.Equal(testEntry.EntryDateString, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        }

        [Fact]
        public void Questions_set_with_ctor_are_not_empty()
        {
            Models.Entry testEntry = new Models.Entry();

            Assert.NotEmpty(testEntry.Questions);
        }

        
    }
}
