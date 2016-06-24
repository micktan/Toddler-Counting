using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    private Announcer Announcer;

	void Start () {
        Announcer = FindObjectOfType<Announcer>();
        Announcer.Announce("Toddler Counting!", 0.0f, 1.1f);
        StartCoroutine(CallToAction());
    }

    IEnumerator CallToAction(float Interval = 1.5f)
    {
        yield return new WaitForSeconds(Interval);
        Announcer.Announce("Click Play to begin!", 0.0f, 1.1f);
        StartCoroutine(CallToAction(8.0f));
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
