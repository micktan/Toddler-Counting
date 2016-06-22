using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    private Announcer Announcer;

	void Start () {
        Announcer = FindObjectOfType<Announcer>();
        Announcer.Announce("Toddler Counting!", 0.0f, 1.1f);
    }

    public void SetGameLevel(string Difficulty)
    {
        PlayerPrefs.SetString("Difficulty", Difficulty);
        Announcer.Announce(Difficulty + "!", 0.0f, 1.1f);
    }

    public void StartLevel(int Level)
    {
        SceneManager.LoadScene(Level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
