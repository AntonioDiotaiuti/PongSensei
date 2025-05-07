using UnityEngine;

public class PlayerShooting : MonoBehaviour, IPlayerShooter
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    [SerializeField] public float projectileSpeed = 10f;
    public string PlayerNumber = "1"; 

    private bool hasAmmo = true;
    private bool triggerHeld = false;
    private string fireAxis;
    private KeyCode keyboardKey;
    private Vector3 shootDirection;

    void Start()
    {
        fireAxis = "Fire" + PlayerNumber.ToString();
        if (PlayerNumber == "1")
        {
            keyboardKey = KeyCode.L;
            shootDirection = Vector3.left;
        }
        else
        {
            keyboardKey = KeyCode.R;
            shootDirection = Vector3.right;
        }
    }

    void Update()
    {
        
        bool keyboardShoot = Input.GetKeyDown(keyboardKey);

        
        float triggerValue = Input.GetAxis(fireAxis);
        bool triggerShoot = triggerValue > 0.5f && !triggerHeld;

        if ((keyboardShoot || triggerShoot) && hasAmmo)
        {
            Shoot();
            triggerHeld = true;
        }

        if (triggerValue < 0.3f)
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
            rb.linearVelocity = shootDirection * projectileSpeed;
        }

        hasAmmo = false;
    }

    public void Reload()
    {
        hasAmmo = true;
    }

    public bool NeedsAmmo()
    {
        return !hasAmmo;
    }
}
