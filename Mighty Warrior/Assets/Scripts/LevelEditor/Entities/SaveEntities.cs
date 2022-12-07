using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public static class SaveEntities
{
    static string substring = "/enemy.dat";

    public static void Save(List<EnemyData> savePrefabs)
    {
        string path = Application.persistentDataPath;

        path = path + substring;

        string saveJson = JsonHelper.ToJson(savePrefabs.ToArray(), true);

        File.WriteAllText(path, saveJson);
        Debug.Log("Map saved: " + path);

    }

    public static List<EnemyData> Load()
    {
        string path = Application.persistentDataPath;
        path = path + substring;

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);

            List<EnemyData> loadPrefabs = JsonHelper.FromJson<EnemyData>(loadJson).ToList<EnemyData>();

            return loadPrefabs;
        }
        else
        {
            Debug.LogError("Save file not found.");
            return null;
        }
    }
}
