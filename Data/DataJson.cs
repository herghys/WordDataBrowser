using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace WordDataBrowser
{
    public enum Difficulties { Easy, Medium, Hard };
    public static class Utilities
    {
        public const string delimiter = ", ";
        public static string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string fullPath;
        public static string fileName;
    }

    public class DataJson
    {
        public WordData wordData = new WordData();
        public DataJson() { }

        #region WriteData
        public static void Write(WordData data, bool saveAs, [Optional]string path)
        {
            #region Save
            if (!saveAs)
            {
                if (!string.IsNullOrEmpty(Utilities.fullPath))
                {
                    File.WriteAllText(Utilities.fullPath, JsonConvert.SerializeObject(data));
                }
                else
                {
                    SaveFileDialog sfd = new SaveFileDialog()
                    {
                        Title = "Create JSON Data",
                        Filter = "JSON File (*.json) | *.json",
                        InitialDirectory = Path.Combine(Utilities.appPath, "JSON Data")
                    };

                    if (sfd.ShowDialog() == true)
                    {
                        path = sfd.FileName;
                        Utilities.fullPath = path;
                        Utilities.fileName = Path.GetFileName(path);
                        File.WriteAllText(path, JsonConvert.SerializeObject(data));
                    }
                }
            }
            #endregion
            #region Save As
            else
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    Title = "Create JSON Data",
                    Filter = "JSON File (*.json) | *.json",
                    InitialDirectory = Path.Combine(Utilities.appPath, "Word Data")
                };

                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(data));
                }
            }
            #endregion
        }
        #endregion

        #region Fetch Data
        public static WordData Fetch()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Open JSON File",
                Filter = "JSON File (*.json) | *.json",
                InitialDirectory = Path.Combine(Utilities.appPath, "Word Data")
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Utilities.fullPath = openFileDialog.FileName;
                Utilities.fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
            }

            WordData wd = new WordData();
            if (!string.IsNullOrEmpty(Utilities.fullPath))
            {
                wd = JsonConvert.DeserializeObject<WordData>(File.ReadAllText(Utilities.fullPath));
            } 
            return wd;
        }
        #endregion
    }
}