using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    static private MusicPlayer _instance = null;

	// Use this for initialization
	void Awake () {
	    if (_instance != null)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
	}
}
