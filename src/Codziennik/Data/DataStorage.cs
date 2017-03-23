using Codziennik.Models;
using Newtonsoft.Json;
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
        public async static Task WriteAllEntries(List<Entry> entriesList)
        {

            IFolder folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("CodziennikFiles", CreationCollisionOption.OpenIfExists);

            IFile file = await folder.CreateFileAsync("entries", CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(JsonConvert.SerializeObject(entriesList));
        }

        public async static Task<List<Entry>> ReadAllEntries()
        {
            IFolder folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("CodziennikFiles", CreationCollisionOption.OpenIfExists);

            if ( (await folder.CheckExistsAsync("entries") ) == ExistenceCheckResult.FileExists )
            {
                IFile file = await folder.GetFileAsync("entries");
                List<Entry> EntriesList = JsonConvert.DeserializeObject<List<Entry>>(await file.ReadAllTextAsync());
                return EntriesList;
            }
            else
                return new List<Entry>();
        }

        public async static Task WriteOneEntry(Entry newEntry)
        {
            List<Entry> entriesList = await ReadAllEntries();

            entriesList.Add(newEntry);

            await WriteAllEntries(entriesList);

        }

        public async static Task DeleteEntry(Entry entryToDelete)
        {
            List<Entry> entriesList = await ReadAllEntries();

            if(entriesList.Remove(entryToDelete))
                await WriteAllEntries(entriesList);
        }
    }
}
