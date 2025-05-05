using UnityEngine;

public class PlayerShootingright : MonoBehaviour, IPlayerShooter
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    [SerializeField] public float projectileSpeed = 10f;
    private bool hasAmmo = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasAmmo)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.right * projectileSpeed;
        }
        hasAmmo = false;
    }

    public void Reload()
    {
        hasAmmo = true;
    }

    public bool HasAmmo => hasAmmo;
}



