using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        IPlayerShooter shooter = other.GetComponent<IPlayerShooter>();

        if (shooter != null)
        {
            shooter.Reload();
            Destroy(gameObject);
        }
    }
}

