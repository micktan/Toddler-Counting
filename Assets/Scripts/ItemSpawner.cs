using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public GameObject[] ItemPrefabs;
    public Vector3 ItemFirstPosition = new Vector3(-4.0f, 0.95f, -10.29f);
    public float MarginX = 2.0f;
    public float MarginZ = 6.0f;
    public float RowOffsetX = 0.5f;
    public int ItemsPerRow = 5;
    public int MaximumRows = 3;

    private LevelManager LevelManager;
    private Vector3[] SpawnPoints;
    private Announcer Announcer;

	// Use this for initialization
	void Start () {
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
    }

    void GenerateSpawnPoints()
    {
        SpawnPoints = new Vector3[MaximumRows * ItemsPerRow];
        Vector3 CurrentPosition = ItemFirstPosition;
        int ArrayIndex = 0;

        for (int row = 0; row < MaximumRows; row++){

            CurrentPosition = ItemFirstPosition + new Vector3(RowOffsetX * row, 0.0f, MarginZ * row);

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
        int RandomIndex = Random.Range(0, ItemPrefabs.Length);
        GameObject ItemPrefab = ItemPrefabs[RandomIndex];

        LevelManager = FindObjectOfType<LevelManager>();
        LevelManager.ItemName = ItemPrefab.transform.name;
        LevelManager.TotalItems = Random.Range(LevelManager.ItemsToCountMin, LevelManager.ItemsToCountMax + 1);
        LevelManager.InputTotal = 0;

        int ItemsSpawned = 0;

        while (ItemsSpawned < LevelManager.TotalItems)
        {
            Instantiate(ItemPrefab, SpawnPoints[ItemsSpawned], Quaternion.identity);
            ItemsSpawned++;
        }
    }
}
