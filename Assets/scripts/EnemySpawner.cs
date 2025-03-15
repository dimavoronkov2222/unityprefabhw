using UnityEngine;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public Transform[] spawnPoints;
    private List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        if (enemies.Count >= maxEnemies) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemies.Add(newEnemy);
    }
    public void RespawnEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
        SpawnEnemy();
    }
}