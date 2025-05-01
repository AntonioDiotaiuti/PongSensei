using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        IPlayerShooter shooter = other.GetComponent<IPlayerShooter>();

        if (shooter != null)
        {
            Debug.Log("Ammo raccolta! Ricarico.");
            shooter.Reload();
            Destroy(gameObject);
        }
    }

}


