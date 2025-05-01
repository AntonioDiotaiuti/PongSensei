using UnityEngine;

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Settings")]
    public float reloadTime = 1f;
    public float projectileSpeed = 10f;

    private float reloadTimer = 0f;
    private bool canShoot = true;
    private int facingDirection = 1; 

    void Update()
    {
        HandleInput();

        
        if (!canShoot)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
            {
                canShoot = true;
            }
        }
    }

    void HandleInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

       
        if (moveInput != 0)
        {
            facingDirection = (int)Mathf.Sign(moveInput);
            transform.localScale = new Vector3(facingDirection, 1, 1);
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
            canShoot = false;
            reloadTimer = reloadTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile proj = bullet.GetComponent<Projectile>();
        proj.speed = projectileSpeed;
        proj.SetDirection(facingDirection);
    }
}
