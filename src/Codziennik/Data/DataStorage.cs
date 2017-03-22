using PCLStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codziennik.Data
{
    class DataStorage
    {
        public string FolderPath { get; private set; }

        public async Task WriteFile()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);

            FolderPath = folder.Path;

            IFile file = await folder.CreateFileAsync("MyFile.txt", CreationCollisionOption.OpenIfExists);

            await file.WriteAllTextAsync("Sample Text...");
        }

     //   public async static Task<string> ReadAllTextAsync(this string fileName, IFolder rootFolder = null)  
     //{  
     //    string content = "";  
     //    IFolder folder = FileSystem.Current.LocalStorage.;  
     //    bool exist = await fileName.IsFileExistAsync(folder);  
     //    if (exist == true)  
     //    {  
     //        IFile file = await folder.GetFileAsync(fileName);  
     //        content = await file.ReadAllTextAsync();  
     //    }  
     //    return content;  
     //}  
    }
}
