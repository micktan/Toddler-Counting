using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerProgress : MonoBehaviour {

    private static PlayerProgress _instance = null;

    // Use this for initialization
    void Start () {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    //it's static so we can call it from anywhere
    public static void Save(Game Game)
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, Game);
        file.Close();
    }

    public static Game Load()
    {
        Game Game = null;
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            Game = (Game)bf.Deserialize(file);
            file.Close();
        }

        return Game;
    }
}