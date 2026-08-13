using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveData(MoneyData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/gamedata.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MoneyData LoadData()
    {
        string path = Application.persistentDataPath + "/gamedata.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MoneyData data = formatter.Deserialize(stream) as MoneyData;

            return data;
        }
        Debug.LogError("No save file");
        return null;
    }
}
