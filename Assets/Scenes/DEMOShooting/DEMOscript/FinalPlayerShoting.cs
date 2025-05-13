using UnityEngine.UI;
using UnityEngine;

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
    [SerializeField] private string fireAxis = "Fire1";
    [SerializeField] private KeyCode keyboardKey = KeyCode.Space;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float triggerValue = Input.GetAxis(fireAxis);  // Dichiarata qui
        bool keyboardShoot = Input.GetKeyDown(keyboardKey);
        bool triggerShoot = triggerValue > 0.5f && !triggerHeld;

        bool isMoving = Vector3.Distance(transform.position, lastPosition) > 0.001f;
        lastPosition = transform.position;

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
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }

        hasAmmo = false;
        reloadBar.fillAmount = 0f;
    }
}
