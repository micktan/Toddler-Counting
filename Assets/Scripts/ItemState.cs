using UnityEngine;
using System.Collections;

public class ItemState : MonoBehaviour {
    public GameObject ItemDefault;
    public GameObject ItemSelected;

    // Use this for initialization
    void Start () {
        SetAnimatedState(false);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SetAnimatedState(bool IsAnimatedState)
    {
        Renderer ItemDefaultRenderer = ItemDefault.GetComponent<Renderer>();
        Renderer ItemSelectedRenderer = ItemSelected.GetComponent<Renderer>();

        if (IsAnimatedState)
        {
            ItemDefaultRenderer.enabled = false;
            ItemSelectedRenderer.enabled = true;
        }
        else
        {
            ItemDefaultRenderer.enabled = true;
            ItemSelectedRenderer.enabled = false;
        }
    }
    /*
    void SetChildObject(GameObject GameObject)
    {

        Destroy(ChildObject);
        ChildObject = Instantiate(NewChild);
        ChildObject.transform.parent = gameObject.transform;
        ChildObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        ChildObject.transform.localEulerAngles = ItemRotation;
        ChildObject.transform.localScale = new Vector3(ScaleFruitModels, ScaleFruitModels, ScaleFruitModels);
    }
        */
}
