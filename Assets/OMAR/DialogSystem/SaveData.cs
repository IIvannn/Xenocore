using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class NPCStateEntry
{
    public string npcID;
    public int state;
}

[System.Serializable]
public class SaveData
{
    public List<NPCStateEntry> npcStates = new List<NPCStateEntry>();
}

public static class SaveSystem //directory for the json file and name
{
    private static string path = Path.Combine(
        Directory.GetParent(Application.dataPath).FullName,
        "save.json"
    );

    public static SaveData Load() //loading the jsonfile
    {
        if (!File.Exists(path))
            return new SaveData();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static void Save(SaveData data) //save the jsonfile
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static void DeleteSave() //delete the json file function
    {
        if (File.Exists(path))
            File.Delete(path);
    }
}
