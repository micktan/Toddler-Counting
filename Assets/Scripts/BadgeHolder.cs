using UnityEngine;
using System.Collections;

public class BadgeHolder : MonoBehaviour
{ 
    public string _prefabResourcePath = "";

    void Start()
    {
    }

    public string PrefabResourcePath
    {
        get { return _prefabResourcePath; }
        set
        {
            _prefabResourcePath = value;
            GameObject Badge = Instantiate(Resources.Load(_prefabResourcePath)) as GameObject;
            Badge.transform.parent = gameObject.transform;
        }
    }
}