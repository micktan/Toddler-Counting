using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public enum GameModes { ClickToCount, AssistedCounting, UnassistedCounting };
    public GameModes GameMode = GameModes.ClickToCount;
    public int ItemsToCountMin = 0;
    public int ItemsToCountMax = 0;
    public int TotalItems = 0;
    public AudioClip AudioCorrectAnswer;
    public int EasyDifficultyMin = 1;
    public int EasyDifficultyMax = 5;
    public int MediumDifficultyMin = 6;
    public int MediumDifficultyMax = 10;
    public int HardDifficultyMin = 11;
    public int HardDifficultyMax = 15;

    private Dictionary<string, int[,]> DifficultyDictionary;
    private Announcer Announcer;
    private int inputTotal = 0;
    private string itemName;
    private string difficulty = "easy";

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);

        DifficultyDictionary = new Dictionary<string, int[,]>();
        DifficultyDictionary.Add("easy", new int[,] { { EasyDifficultyMin, EasyDifficultyMax } });
        DifficultyDictionary.Add("medium", new int[,] { { MediumDifficultyMin, MediumDifficultyMax } });
        DifficultyDictionary.Add("hard", new int[,] { { HardDifficultyMin, HardDifficultyMax } });

    }

    // Update is called once per frame
    void Update () {
	
	}
    public string Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value.ToLower();
            ItemsToCountMin = DifficultyDictionary[difficulty][0, 0];
            ItemsToCountMax = DifficultyDictionary[difficulty][0, 1];
        }
    }

    public string ItemName
    {
        get
        {
            return itemName;
        }
        set
        {
            itemName = value.ToLower();
            if (itemName == "kiwi")
            {
                itemName += " fruits";
            }
            Announcer = FindObjectOfType<Announcer>();
            Announcer.Announce("Count the " + itemName + "!", 0.0f, 1.1f);
        }
    }

    public int InputTotal
    {
        get
        {
            return inputTotal;
        }
        set
        {
            inputTotal = value;
            if ((GameMode == GameModes.ClickToCount) && (inputTotal > 0))
            {
                Announcer = FindObjectOfType<Announcer>();
                Announcer.Announce(inputTotal.ToString());

                if (inputTotal == TotalItems)
                {
                    Announcer.Announce("Awesome! There are " + inputTotal + " " + itemName + ".", 1.0f);
                }
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
