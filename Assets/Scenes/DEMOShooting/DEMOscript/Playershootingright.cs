using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingright : MonoBehaviour, IPlayerShooter
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float reloadTime = 3f; // tempo richiesto per ricaricare da fermi
    public Image reloadBarUI;     // da assegnare nell'Inspector

    private bool hasAmmo = true;
    private float reloadTimer = 0f;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        if (reloadBarUI != null)
            reloadBarUI.fillAmount = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasAmmo)
        {
            Shoot();
        }

        HandlePassiveReload();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.right * projectileSpeed; // correggi da linearVelocity a velocity
        }
        hasAmmo = false;

        if (reloadBarUI != null)
            reloadBarUI.fillAmount = 0f;

        reloadTimer = 0f;
    }

    void HandlePassiveReload()
    {
        if (hasAmmo) return;

        bool isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f;

        if (!isMoving)
        {
            reloadTimer += Time.deltaTime;
            if (reloadBarUI != null)
                reloadBarUI.fillAmount = reloadTimer / reloadTime;

            if (reloadTimer >= reloadTime)
            {
                Reload();
            }
        }
        else
        {
            reloadTimer = 0f;
            if (reloadBarUI != null)
                reloadBarUI.fillAmount = 0f;
        }

        lastPosition = transform.position;
    }

    public void Reload()
    {
        hasAmmo = true;
        reloadTimer = 0f;

        if (reloadBarUI != null)
            reloadBarUI.fillAmount = 1f;
    }

    public bool HasAmmo => hasAmmo;
}



