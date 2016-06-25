using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenuManager : MonoBehaviour {

    private float SideMenuCloseX;
    private float SideMenuOpenX;

    // Use this for initialization
    void Start () {
        SetupSideMenuDimensions();
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
        float TotalWait = 2.5f;
        float Interval = 0.01f;
        float AccumulatedWait = 0.0f;
        RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform TimeoutBarRect = GameObject.Find("TimeoutBar").GetComponent<RectTransform>();
        RectTransform ButtonSideMenuRect = GameObject.Find("ButtonSideMenu").GetComponent<RectTransform>();
        float TimeoutBarMaxWidth = CanvasRect.sizeDelta.x - ButtonSideMenuRect.sizeDelta.x;

        while (AccumulatedWait <= TotalWait)
        {
            AccumulatedWait += Interval;
            float ProgressWidth = (1 - AccumulatedWait / TotalWait) * TimeoutBarMaxWidth;
            TimeoutBarRect.sizeDelta = new Vector2(ProgressWidth, 12);
            yield return new WaitForSeconds(Interval);
        }
        SceneManager.LoadScene(LevelIndex);
    }

    void SetupSideMenuDimensions()
    {
        RectTransform SideMenuRect = GameObject.Find("SideMenu").GetComponent<RectTransform>();
        RectTransform ButtonSideMenuRect = GameObject.Find("ButtonSideMenu").GetComponent<RectTransform>();
        GameObject SideMenu = GameObject.Find("SideMenu");

        float SlideDistance = SideMenuRect.sizeDelta.x - ButtonSideMenuRect.sizeDelta.x;
        SideMenuCloseX = SideMenu.transform.localPosition.x;
        SideMenuOpenX = SideMenu.transform.localPosition.x - SlideDistance;
    }

    public void ToggleSideMenu(bool ForceClose = false)
    {
        GameObject SideMenu = GameObject.Find("SideMenu");

        if (!ForceClose && SideMenu.transform.localPosition.x == SideMenuCloseX)
        {
            //open
            SideMenu.transform.localPosition = new Vector3(SideMenuOpenX, SideMenu.transform.localPosition.y);
        }
        else
        {
            //close
            SideMenu.transform.localPosition = new Vector3(SideMenuCloseX, SideMenu.transform.localPosition.y);
        }
    }
}
