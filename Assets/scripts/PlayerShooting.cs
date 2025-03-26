using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;
    public int ammo = 30;
    private float nextFireTime = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime && ammo > 0)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        if (ammo <= 0) return;
        GameObject bulletInstance = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = bulletInstance.AddComponent<Bullet>();
        bulletScript.speed = bulletSpeed;
        bulletScript.damage = 5;
        ammo--;
        Debug.Log("Осталось патронов: " + ammo);
    }
    public void AddAmmo(int amount)
    {
        ammo += amount;
        Debug.Log("Добавлено патронов: " + amount + ". Всего патронов: " + ammo);
    }
}