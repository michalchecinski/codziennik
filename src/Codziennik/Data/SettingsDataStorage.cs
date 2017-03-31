using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codziennik.Data
{
    class SettingsDataStorage
    {
        public async static Task WriteAllQuestions(List<string> settingsList)
        {

            IFolder folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("CodziennikFiles", CreationCollisionOption.OpenIfExists);

            IFile file = await folder.CreateFileAsync("settings", CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(JsonConvert.SerializeObject(settingsList));
        }

        public async static Task<List<string>> ReadAllQuestions()
        {
            IFolder folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("CodziennikFiles", CreationCollisionOption.OpenIfExists);

            if ((await folder.CheckExistsAsync("settings")) == ExistenceCheckResult.FileExists)
            {
                IFile file = await folder.GetFileAsync("settings");
                List<string> settingsList = JsonConvert.DeserializeObject<List<string>>(await file.ReadAllTextAsync());
                return settingsList;
            }
            else
                return new List<string>();
        }
    }
}
