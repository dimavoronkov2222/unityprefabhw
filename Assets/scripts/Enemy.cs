using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float speed = 3f;
    public GameObject corpsePrefab;
    public GameObject enemyPrefab;
    public Transform player;
    public int maxSpawnCount = 5;
    private static int totalEnemies = 0;
    private static int maxEnemies = 10;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        totalEnemies++;
    }
    private void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (corpsePrefab != null)
        {
            Instantiate(corpsePrefab, transform.position, transform.rotation);
        }
        int spawnCount = Random.Range(1, maxSpawnCount + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            if (totalEnemies < maxEnemies)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
        totalEnemies--;
        Destroy(gameObject);
    }
}