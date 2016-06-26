using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;



public class LevelMenuManager : MonoBehaviour {

    private float SideMenuCloseX;
    private float SideMenuOpenX;
    private PlayerProgress PlayerProgress;
    // Use this for initialization
    void Start () {
        SetupSideMenuDimensions();
        PlayerProgress = FindObjectOfType<PlayerProgress>();
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

    public void SaveGame()
    {
        Game Game = new Game();

        BadgeManagerSetup BadgeManagerSetup = FindObjectOfType<BadgeManagerSetup>();
        if (BadgeManagerSetup)
        {
            Game.Badges = new List<BadgeStruct>();

           foreach(GameObject Badge in GameObject.FindGameObjectsWithTag("Badge"))
            {
                BadgeStruct SaveData = new BadgeStruct();
                GameObject BadgeHolderGameObject = Badge.transform.parent.gameObject;
                BadgeHolder BadgeHolder = BadgeHolderGameObject.GetComponent<BadgeHolder>();

                string PrefabPath = BadgeHolder.PrefabResourcePath;
                Debug.Log("1st PrefabPath:" + PrefabPath);
                if (PrefabPath == "")
                { 
                    Object Prefab = PrefabUtility.GetPrefabParent(Badge);
                    PrefabPath = AssetDatabase.GetAssetPath(Prefab).Replace("Assets/Resources/", "").Replace(".prefab", "");
                    Debug.Log("2nd PrefabPath:" + PrefabPath);

                }

                SaveData.Name = BadgeHolderGameObject.transform.name;
                SaveData.PrefabResourcePath = PrefabPath;
                SaveData.LocalPosition = BadgeHolderGameObject.transform.localPosition;
                SaveData.LocalRotation = BadgeHolderGameObject.transform.localRotation;

                Game.Badges.Add(SaveData);
                Debug.Log("Save data:" + SaveData.Name + " path " + SaveData.PrefabResourcePath);
            }
        }

        PlayerProgress.Save(Game);
    }

    public void LoadGame()
    {
        Game Game = PlayerProgress.Load();

        if (Game != null)
        {
            foreach (BadgeHolder Badge in FindObjectsOfType<BadgeHolder>())
            {
                Destroy(Badge.gameObject);
            }

            BadgeManagerSetup BadgeManagerSetup = FindObjectOfType<BadgeManagerSetup>();
            
            foreach (BadgeStruct BadgeStruct in Game.Badges)
            {
                Debug.Log("Loading data " + BadgeStruct.Name + " path " + BadgeStruct.PrefabResourcePath);
                GameObject NewBadge = Instantiate(BadgeManagerSetup.BadgePrefab);
                NewBadge.transform.name = BadgeStruct.Name;
                NewBadge.transform.parent = BadgeManagerSetup.transform;

                BadgeHolder BadgeHolder = NewBadge.GetComponent<BadgeHolder>();
                BadgeHolder.PrefabResourcePath = BadgeStruct.PrefabResourcePath;

                NewBadge.transform.localPosition = BadgeStruct.LocalPosition;
                NewBadge.transform.localRotation = BadgeStruct.LocalRotation;
            }
        }
    }
}
