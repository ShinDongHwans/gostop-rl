using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

public class GameSaveManager { 
    public GameSaveManager instance;
    public List<List<GameReport>> loaded_hubo; 
    public static string a;

    public void Init()
    {
        loaded_hubo = new List<List<GameReport>>(21);

        for (int k = 0; k < 21; k++)
        {
            loaded_hubo.Add(new List<GameReport>());
        }

        //if (instance == null) instance = this;
        //else if (instance != this) Destroy(this);
        //DontDestroyOnLoad(this);
        //a = Application.persistentDataPath;
        a = "C:/Users/q/AppData/LocalLow/KAIST/GoStop";
    }
 
    public static bool IsSaveFile()
    {
        return Directory.Exists(a + "/game_save");
    }

    public void SaveGame(List<List<GameReport>> hubos)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(a + "/game_save");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(a + "/game_save/data1.txt");
        var json = JsonUtility.ToJson(hubos);
        bf.Serialize(file, json);
        file.Close();
    }

    public List<List<GameReport>> LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(a + "/game_save/data1.txt"))
        {
            FileStream file = File.Open(a + "/game_save/data1.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), loaded_hubo);
            file.Close();
        }
        return loaded_hubo;
    }
}