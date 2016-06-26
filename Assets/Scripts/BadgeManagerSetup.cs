using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BadgeManagerSetup : MonoBehaviour {

    public float TouchHoldDuration = 0.25f;
    public GameObject BadgePrefab;

    private GameObject TouchedGameObject = null;
    private float TouchDuration;
 
	// Use this for initialization
	void Start () {
	}

    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        if (Input.touchCount == 0) //  && !EventSystem.current.IsPointerOverGameObject(-1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                //                LevelMenuManager LevelMenuManager = FindObjectOfType<LevelMenuManager>();
                //                LevelMenuManager.ToggleSideMenu(true);
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }
    }

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        RaycastHit RaycastHit;
        Ray Ray = new Ray(touchPosition, Vector3.forward);

        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (Physics.Raycast(Ray, out RaycastHit))
                {
                    GameObject GameObject = RaycastHit.transform.gameObject;
                    TouchedGameObject = GameObject;
                    TouchDuration = 0.0f;
                }else
                {
                    TouchedGameObject = null; 
                }
                break;
            case TouchPhase.Moved:
                if (TouchedGameObject)
                {
                    if (TouchDuration >= TouchHoldDuration)
                    {
                        //Holding object
                        TouchedGameObject.transform.localPosition = new Vector3(touchPosition.x, touchPosition.y);
                    }
                    else
                    {
                        if (Physics.Raycast(Ray, out RaycastHit))
                        {
                            GameObject GameObject = RaycastHit.transform.gameObject;
                            if (TouchedGameObject == GameObject)
                            {
                                TouchDuration += Time.deltaTime;
                            }
                        }
                    }
                }
                break;
            case TouchPhase.Ended:
                if (TouchedGameObject && TouchDuration >= TouchHoldDuration)
                {
                    TouchedGameObject = null;
                } else
                {
                    //Touch or click event
                    if (Physics.Raycast(Ray, out RaycastHit))
                    {
                        GameObject GameObject = RaycastHit.transform.gameObject;
                        Debug.Log("GameObject Transform Position: " + GameObject.transform.position);
                        Debug.Log("Touch Position: " + touchPosition);
                    }
                }


                break;
        }

    }
}
