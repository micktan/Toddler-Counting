using UnityEngine;
using System.Collections;

public class ClickToCountClickHandler : MonoBehaviour {
    public GameObject ItemDefault;
    public GameObject ItemSelected;
    public bool Clicked = false;

    static private int ItemsCounted = 0;
    private Announcer Announcer;
    private ClickToCountSetup ClickToCountSetup;

    void Awake()
    {
        ClickToCountSetup = FindObjectOfType<ClickToCountSetup>();
        if (ClickToCountSetup) {
            ItemDefault.AddComponent<ClickToCountItem>().ClickToCountClickHandler = this;
            ItemSelected.AddComponent<ClickToCountItem>().ClickToCountClickHandler = this;
        }
    }

    // Use this for initialization
    void Start () {
        Announcer = FindObjectOfType<Announcer>();
        HideChild(ItemSelected);
        ShowChild(ItemDefault);
        ItemsCounted = 0;
    }

    // Update is called once per frame
    void Update () {
    }

    void ToggleClickState()
    {
        if (!Clicked)
        {
            HideChild(ItemDefault);
            ShowChild(ItemSelected);
        }
        else 
        {
            ShowChild(ItemDefault);
            HideChild(ItemSelected);
        }
        Clicked = !Clicked;
    }

    void HideChild(GameObject GameObject)
    {
        Renderer Renderer = GameObject.GetComponent<Renderer>();
        Renderer.enabled = false;

        MeshCollider MeshCollider = GameObject.GetComponent<MeshCollider>();
        if (MeshCollider)
        {
            Destroy(MeshCollider);
        }
    }

    void ShowChild(GameObject GameObject)
    {
        Renderer Renderer = GameObject.GetComponent<Renderer>();
        Renderer.enabled = true;

        Rigidbody Rigidbody = GameObject.GetComponent<Rigidbody>();
        if (!Rigidbody)
        {
            GameObject.AddComponent<Rigidbody>();
        }

        Rigidbody = GameObject.GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;

        MeshCollider MeshCollider = GameObject.GetComponent<MeshCollider>();
        if (!MeshCollider)
        {
            MeshCollider = GameObject.AddComponent<MeshCollider>();
            MeshCollider.convex = true;
        }

    }

    public void CorrectClick()
    {
        ItemsCounted++;
        Announcer.Announce(ItemsCounted.ToString());

        if (ItemsCounted == ClickToCountSetup.ItemsToCount)
        {
            string[] Praises = { "Awesome!", "Great job!", "Nice going!", "Well done!", "Perfect!" };
            int RandomIndex = Random.Range(0, Praises.Length);
            Announcer.Announce(Praises[RandomIndex] + " There are " + ItemsCounted + " " + ClickToCountSetup.ItemPrefab.transform.name.ToLower() + ".", 1.0f);
            LevelMenuManager LevelMenuManager = FindObjectOfType<LevelMenuManager>();
            LevelMenuManager.LevelComplete();
        }
    }

    public void ChildClickHandler()
    {
        if (!Clicked)
        {
            ToggleClickState();
            CorrectClick();
        }
    }
}

public class ClickToCountItem : MonoBehaviour
{
    public ClickToCountClickHandler ClickToCountClickHandler;
    
    public void TouchTouchPhaseEnded()
    {
        ClickToCountClickHandler.ChildClickHandler();
    }
}