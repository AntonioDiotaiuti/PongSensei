using UnityEngine;

public class Ricarica : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayerShooter shooter = collision.GetComponent<IPlayerShooter>();
        if (shooter != null)
        {
            shooter.Reload();
            Destroy(gameObject); 
        }
    }
}
