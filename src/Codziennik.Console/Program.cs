using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Codziennik.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Entry entry = new Models.Entry("entry content");
            string serialized = JsonConvert.SerializeObject(entry);

            System.Console.WriteLine("Data: " + entry.EntryDateString);

            System.Console.WriteLine(serialized);

            System.Console.ReadKey();
            
        }
    }
}
