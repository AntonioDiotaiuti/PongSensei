using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Image reloadBar;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] private float reloadTime = 2f;
    public string PlayerNumber = "1";

    private bool hasAmmo = true;
    private bool triggerHeld = false;
    private float reloadTimer = 0f;
    private bool isReloading = false;
    private string fireAxis;
    private KeyCode keyboardKey;
    private Vector3 shootDirection;

    void Start()
    {
        fireAxis = "Fire" + PlayerNumber;
        if (PlayerNumber == "1")
        {
            keyboardKey = KeyCode.R;
            shootDirection = Vector3.left;
        }
        else
        {
            keyboardKey = KeyCode.Mouse0;
            shootDirection = Vector3.right;
        }
    }

    void Update()
    {
        bool keyboardShoot = Input.GetKeyDown(keyboardKey);
        float triggerValue = Input.GetAxis(fireAxis);
        bool triggerShoot = triggerValue > 0.5f && !triggerHeld;
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if ((keyboardShoot || triggerShoot) && hasAmmo)
        {
            Shoot();
            triggerHeld = true;
        }

        if (triggerValue < 0.3f)
        {
            triggerHeld = false;
        }

        if (!hasAmmo)
        {
            if (isMoving)
            {
                reloadTimer = 0f;
                reloadBar.fillAmount = 0f;
            }
            else
            {
                reloadTimer += Time.deltaTime;
                reloadBar.fillAmount = reloadTimer / reloadTime;

                if (reloadTimer >= reloadTime)
                {
                    hasAmmo = true;
                    reloadTimer = 0f;
                    reloadBar.fillAmount = 1f;
                }
            }
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
        reloadBar.fillAmount = 0f;
    }
}
