using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponMaker
{
    public class PreferencesService : IService
    {
        public Preferences Preferences { get; set; }

        private string _preferencesFilePath;

        private const string PREFERENCES_FOLDER = "Preferences";
        private const string PREFERENCES_FILE_NAME = "preferencesData.json";

        public PreferencesService()
        {
            //Check and create folder if needed
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            basePath = Path.Combine(basePath, PREFERENCES_FOLDER);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            //Check and create file if needed
            string fileName = PREFERENCES_FILE_NAME;
            string filePath = Path.Combine(basePath, fileName);
            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }
            _preferencesFilePath = filePath;

            //Load preferences
            string input = "";
            using (StreamReader sw = new StreamReader(filePath))
            {
                input = sw.ReadToEnd();
            }
            var rawPrefs = JsonConvert.DeserializeObject<Preferences>(input);
            Preferences = rawPrefs ?? new Preferences();
        }

        internal void PreferencesDialog_Closed(object sender, CancelEventArgs e)
        {
            FileSystemService.SaveToJson(Preferences, _preferencesFilePath);
        }

        internal void Project_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Name" && e.PropertyName != "Path") return;

            var project = sender as Project;
            Preferences.LastProjectName = project.Name;
            Preferences.LastProjectPath = project.Path;

            FileSystemService.SaveToJson(Preferences, _preferencesFilePath);
        }
    }
}
