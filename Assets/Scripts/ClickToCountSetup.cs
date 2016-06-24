using UnityEngine;
using System.Collections;

public class ClickToCountSetup : MonoBehaviour {

    public int ItemsToCount = 0;
    public GameObject ItemPrefab;
    public Vector3 ItemFirstPosition = new Vector3(-4.0f, 0.95f, -10.29f);
    public float MarginX = 0.0f;
    public float MarginY = 0.0f;
    public float MarginZ = 0.0f;
    public float RowOffsetX = 0.5f;
    public int ItemsPerRow = 5;
    public int MaximumRows = 3;

    private Announcer Announcer;
    private Vector3[] SpawnPoints;

	// Use this for initialization
	void Start () {
        Announcer = FindObjectOfType<Announcer>();
        StartCoroutine(InitialiseItems());
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator InitialiseItems()
    {
        yield return new WaitForSeconds(0.25f);
        GenerateSpawnPoints();
        SpawnItems();
        Announcer.Announce("Count the " + ItemPrefab.transform.name.ToLower() + "!");
    }

    void GenerateSpawnPoints()
    {
        SpawnPoints = new Vector3[MaximumRows * ItemsPerRow];
        Vector3 CurrentPosition = ItemFirstPosition;
        int ArrayIndex = 0;

        for (int row = 0; row < MaximumRows; row++){

            CurrentPosition = ItemFirstPosition + new Vector3(RowOffsetX * row, MarginY * row, MarginZ * row);

            for (int column = 0; column < ItemsPerRow; column++)
            {
                if ((column != 0))
                {
                    CurrentPosition = CurrentPosition + new Vector3(MarginX, 0.0f, 0.0f);
                }

                SpawnPoints[ArrayIndex] = CurrentPosition;
                ArrayIndex++;
            }
        }
    }

    void SpawnItems()
    {
        int ItemsSpawned = 0;

        while (ItemsSpawned < ItemsToCount)
        {
            GameObject instance = Instantiate(ItemPrefab);
            instance.transform.position = SpawnPoints[ItemsSpawned];
            //, Quaternion.identity);
            ItemsSpawned++;
        }
    }

}


