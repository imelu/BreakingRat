using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(List<ThingyData> _data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data2.rat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, _data);
        stream.Close();

        //Debug.Log("saving");
    }

    public static List<ThingyData> LoadData()
    {
        string path = Application.persistentDataPath + "/data2.rat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<ThingyData> _data = formatter.Deserialize(stream) as List<ThingyData>;

            return _data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
