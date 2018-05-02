using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadJSON : MonoBehaviour {

    public static GameStatus LoadJsonFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.dataPath + "/Resources/data.json"))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/data.json");

        if (sr == null)
        {
            return null;
        }

        string json = sr.ReadToEnd();

        if (json.Length > 0)
        {
            return JsonUtility.FromJson<GameStatus>(json);
        }

        return null;
    }

}
