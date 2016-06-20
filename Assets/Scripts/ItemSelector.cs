using UnityEngine;
using System.Collections;

public class ItemSelector : MonoBehaviour {

    public Material IndicatorGood;
    public Material IndicatorBad;
    public AudioClip AudioGoodSelection;
    public AudioClip AudioBadSelection;

    private LevelManager LevelManager;
    private AudioSource AudioSource;
    private bool HasBeenCounted = false;
    private GameObject Indicator;

	// Use this for initialization
	void Start () {
        LevelManager = FindObjectOfType<LevelManager>();
        AudioSource = gameObject.GetComponent<AudioSource>();
        Indicator = transform.Find("Indicator").gameObject;
    }

    // Update is called once per frame
    void Update () {

    }
 
    void OnMouseDown()
    {

        if (LevelManager.GameMode == LevelManager.GameModes.ClickToCount)
        {
            if (HasBeenCounted)
            {
                BadSelection();
            }
            else
            {
                GoodSelection();
            }
        }
    }

    void GoodSelection()
    {
        AudioSource.PlayOneShot(AudioGoodSelection);
        StartCoroutine(HighlightObject(true));
        HasBeenCounted = true;
        LevelManager.InputTotal++;
        ItemState ItemState = gameObject.GetComponent<ItemState>();
        ItemState.SetAnimatedState(true);
    }

    void BadSelection()
    {
        AudioSource.PlayOneShot(AudioBadSelection);
        StartCoroutine(HighlightObject(false));
    }

    IEnumerator HighlightObject(bool IsGoodSelection)
    {
        float HighlightDuration = 0.3f;
        Renderer Renderer = Indicator.GetComponent<Renderer>();

        if (IsGoodSelection)
        {
            Renderer.material = IndicatorGood;
            Renderer.enabled = true;
            yield return new WaitForSeconds(HighlightDuration);
            Renderer.enabled = false;
        }
        else
        {
            Renderer.material = IndicatorBad;
            Renderer.enabled = true;
            yield return new WaitForSeconds(HighlightDuration);
            Renderer.enabled = false;
        }
    }
}
