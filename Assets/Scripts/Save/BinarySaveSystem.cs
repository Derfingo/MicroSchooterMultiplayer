using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Save
{
    public class BinarySaveSystem : ISaveSystem
    {
        private readonly string _filePath;

        public BinarySaveSystem()
        {
            _filePath = Application.persistentDataPath + "/SaveSetting.dat";
        }

        public void Save(SettingsData data)
        {
            using FileStream file = File.Create(_filePath);
            new BinaryFormatter().Serialize(file, data);
        }

        public SettingsData Load()
        {
            SettingsData saveData;

            if (File.Exists(_filePath))
            {
                using (FileStream file = File.Open(_filePath, FileMode.Open))
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    saveData = (SettingsData)loadedData;
                }

                Debug.Log("saved data");
                return saveData;
            }
            else
            {
                Debug.Log("default data");
                return null;
            }
        }
    }
}
