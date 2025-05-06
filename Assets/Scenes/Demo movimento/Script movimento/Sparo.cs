using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Vector2 bulletDirection = Vector2.right;
    public KeyCode shootKey = KeyCode.Space;

    private bool hasBullet = true;

    void Update()
    {
        if (Input.GetKeyDown(shootKey) && hasBullet)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = bulletDirection;
        hasBullet = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            hasBullet = true;
            Destroy(collision.gameObject);
        }
    }
}
