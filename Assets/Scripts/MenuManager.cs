using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    private Announcer Announcer;

	// Use this for initialization
	void Start () {
        Announcer = FindObjectOfType<Announcer>();
        Announcer.Announce("Toddler Counting!", 0.0f, 1.1f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SetGameLevel(string Difficulty)
    {
        StartCoroutine(AnnounceBeforeLaunching(Difficulty));
    }

    IEnumerator AnnounceBeforeLaunching(string Difficulty)
    {
        Announcer.Announce(Difficulty + "!", 0.0f, 1.1f);
        LevelManager LevelManager = FindObjectOfType<LevelManager>();
        LevelManager.Difficulty = Difficulty;
        yield return new WaitForSeconds(0.5f);
        LevelManager.LoadNextScene();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
