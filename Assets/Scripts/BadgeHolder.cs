using UnityEngine;
using System.Collections;

public class BadgeHolder : MonoBehaviour
{ 
    public string prefabResourcePath = "";

    public string PrefabResourcePath
    {
        get { return prefabResourcePath; }
        set
        {
            prefabResourcePath = value;
            GameObject Badge = Instantiate(Resources.Load(prefabResourcePath)) as GameObject;
            Badge.transform.parent = gameObject.transform;
        }
    }
}