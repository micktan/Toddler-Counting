using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GotoNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelComplete()
    { 
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextScene > SceneManager.sceneCountInBuildSettings - 1){
            NextScene = 0;
        }
        StartCoroutine(PauseAndLoadLevel(NextScene));
    }

    IEnumerator PauseAndLoadLevel(int LevelIndex)
    {
        float TotalWait = 4.0f;
        float Interval = 0.01f;
        float AccumulatedWait = 0.0f;
        RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform TimeoutBarRect = GameObject.Find("TimeoutBar").GetComponent<RectTransform>();

        while (AccumulatedWait <= TotalWait)
        {
            AccumulatedWait += Interval;
            float ProgressWidth = (1 - AccumulatedWait / TotalWait) * CanvasRect.sizeDelta.x;
            TimeoutBarRect.sizeDelta = new Vector2(ProgressWidth, 12);
            yield return new WaitForSeconds(Interval);
        }
        SceneManager.LoadScene(LevelIndex);
    }
}
