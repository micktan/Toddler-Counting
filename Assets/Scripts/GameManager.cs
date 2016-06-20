using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetGameLevel(string Difficulty = null)
    {
        LevelManager LevelManager = FindObjectOfType<LevelManager>();
        if ((Difficulty != null) && (Difficulty.Length > 0))
        {
            LevelManager.Difficulty = Difficulty;
        }
        LevelManager.ReloadScene();
    }

    public void GoToMenu()
    {
        LevelManager LevelManager = FindObjectOfType<LevelManager>();
        LevelManager.LoadMenuScene();
    }

}
