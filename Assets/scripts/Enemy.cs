using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float speed = 3f;
    public Transform player;
    public GameObject corpsePrefab;
    private int hitCount = 0;
    private bool isWounded = false;
    private float fireRate = 2f;
    private float nextFireTime = 0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        hitCount++;
        if (hitCount >= 10)
        {
            isWounded = true;
            speed /= 2;
        }
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
        Destroy(gameObject);
    }
    private void Shoot()
    {
        if (player == null) return;
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.position = transform.position + transform.forward;
        bullet.transform.localScale = Vector3.one * 0.2f;
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        SphereCollider collider = bullet.GetComponent<SphereCollider>();
        collider.isTrigger = true;
        EnemyBullet bulletScript = bullet.AddComponent<EnemyBullet>();
        bulletScript.speed = 10f;
        bulletScript.damage = 10;
        Vector3 direction = (player.position - transform.position).normalized;
        bullet.transform.forward = direction;
        rb.linearVelocity = direction * bulletScript.speed;
    }
}