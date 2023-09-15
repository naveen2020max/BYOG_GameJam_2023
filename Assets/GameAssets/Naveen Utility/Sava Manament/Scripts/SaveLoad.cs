using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Naveen
{
    public static class SaveLoad
    {
        public static void Save<T>(DataInfo<T> data) where T : class, ISaveID, new()
        {
            T saveData = data.saveData;

            string JsonString = JsonUtility.ToJson(saveData);

            StreamWriter sw = new StreamWriter(data.filePath);

            sw.Write(JsonString);

            sw.Close();
        }

        public static T Load<T>(DataInfo<T> data) where T : class, ISaveID, new()
        {
            if (File.Exists(data.filePath))
            {
                StreamReader sr = new StreamReader(data.filePath);

                string JsonString = sr.ReadToEnd();

                sr.Close();

                T savedata = JsonUtility.FromJson<T>(JsonString);

                return savedata;
            }
            else
            {
                return new T();
            }
        }
    }


    public class DataInfo<T> where T : class, ISaveID, new()
    {
        public string FileType;

        string FileName;


        public T saveData;

        public string filePath;

        public DataInfo(string _filename)
        {
            FileName = _filename;
            saveData = new T();
            FileType = saveData.GetType().ToString();
            if (!SetData())
                Debug.Log("No File Name");

        }

        public bool SetData()
        {
            if (!string.IsNullOrWhiteSpace(FileName))
            {
                filePath = Application.persistentDataPath + "/" + FileName + ".nvn";
                Debug.Log(filePath);
                return true;
            }
            return false;
        }

        public void SaveData(T _data)
        {
            saveData = _data;
            SaveLoad.Save(this);
            Debug.Log("saved");
        }

        public T LoadData()
        {
            saveData = SaveLoad.Load(this);
            Debug.Log("Loaded");

            return saveData;
        }
    }

    // for security purpose
    public interface ISaveID
    {

    }
}
