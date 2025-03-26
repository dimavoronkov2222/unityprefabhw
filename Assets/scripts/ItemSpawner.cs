using UnityEngine;
using System.Collections;
public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float spawnInterval = 10f;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    private void Start()
    {
        StartCoroutine(SpawnItems());
    }
    private IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomItem();
        }
    }
    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length == 0) return;
        int itemIndex = Random.Range(0, itemPrefabs.Length);
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            spawnAreaMin.y,
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );
        Instantiate(itemPrefabs[itemIndex], randomPosition, Quaternion.identity);
    }
}