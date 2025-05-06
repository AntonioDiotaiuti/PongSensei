using UnityEngine;

public class PlayerShootingleft : MonoBehaviour, IPlayerShooter
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    [SerializeField] public float projectileSpeed = 10f;
    private bool hasAmmo = true;
    private bool triggerHeld = false;

    void Update()
    {
        // Tasto tastiera
        bool keyboardShoot = Input.GetKeyDown(KeyCode.L);

        // L2 controller (3rd axis)
        float l2Value = Input.GetAxis("FireL2");
        bool controllerShoot = l2Value > 0.5f && !triggerHeld;

        if ((keyboardShoot || controllerShoot) && hasAmmo)
        {
            Shoot();
            triggerHeld = true; // previene spam mentre L2 è tenuto premuto
        }

        // reset quando L2 rilasciato
        if (l2Value < 0.3f)
        {
            triggerHeld = false;
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
