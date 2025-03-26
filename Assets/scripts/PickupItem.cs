using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum ItemType { Health, Ammo }
    public ItemType itemType;
    public int value;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                if (itemType == ItemType.Health)
                {
                    player.AddHealth(value);
                }
                else if (itemType == ItemType.Ammo)
                {
                    PlayerShooting shooting = player.GetComponent<PlayerShooting>();
                    if (shooting != null)
                    {
                        shooting.AddAmmo(value);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}