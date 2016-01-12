using System;
using System.IO;
using System.IO.Compression;
using System.Web.Script.Serialization;

namespace RuleSetEditor
{
    public class Settings
    {
        private static Lazy<Settings> settings = new Lazy<Settings>(() =>
        {
            if (!SettingsFile.Exists)
            {
                return new Settings()
                {
                    LastFile = string.Empty
                };
            }
            else
            {
                using (var fileStream = SettingsFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var compressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
                using (var reader = new StreamReader(compressionStream))
                {
                    var serializer = new JavaScriptSerializer();
                    return serializer.Deserialize<Settings>(reader.ReadToEnd());
                }
            }
        });
        private static FileInfo SettingsFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RuleSetEditor.settings"));

        public static Settings Default
        {
            get
            {
                return settings.Value;
            }
        }

        public string LastFile { get; set; }

        public void Save()
        {
            using (var fileStream = SettingsFile.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
            using (var writer = new StreamWriter(compressionStream))
            {
                var serializer = new JavaScriptSerializer();
                writer.Write(serializer.Serialize(this));
            }
        }
    }
}
