using UnityEngine;

using UnityEngine;

public class PlayerShootingleft : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    [SerializeField]public float projectileSpeed = 10f;
    private bool hasAmmo = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && hasAmmo)
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
            rb.linearVelocity = Vector3.left * projectileSpeed;
        }
        hasAmmo = false;
    }
    public void Reload()
    {
            hasAmmo = true;
    }
}