using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;
    private float nextFireTime = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = bulletInstance.AddComponent<Bullet>();
        bulletScript.speed = bulletSpeed;
        bulletScript.damage = 5;
    }
}