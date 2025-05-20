using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float CooldownInput = 0.3f;

    [SerializeField] private string playerNumber = "1";
    [SerializeField] private KeyCode keyboardKey = KeyCode.R;

    public bool inputEnable = true;
    private bool triggerHeld = false;
    private string fireAxis;
    private Vector3 shootDirection;

    private ReloadSystem reloadSystem;

    void Start()
    {
        reloadSystem = GetComponent<ReloadSystem>();

        fireAxis = "Fire" + playerNumber;

        if (playerNumber == "1")
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
        if (!inputEnable) return;

        bool keyboardShoot = Input.GetKeyDown(keyboardKey);
        float triggerValue = Input.GetAxis(fireAxis);
        bool triggerShoot = triggerValue > 0.5f && !triggerHeld;

        if ((keyboardShoot || triggerShoot) && reloadSystem.HasAmmo())
        {
            inputEnable = false;
            Fire();
            triggerHeld = true;
            StartCoroutine(EnableInput(CooldownInput));
        }

        if (triggerValue < 0.3f)
        {
            triggerHeld = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        var bulletComp = bullet.GetComponent<Bullet>();
        if (bulletComp != null)
        {
            bulletComp.direction = firePoint.right;
        }

        reloadSystem.ConsumeAmmo();
    }

    private void OnDeath()
    {
        Destroy(gameObject);

        //Vittoria + blocco gioco
        GameManager.Instance.DeclareVictory("Player"+playerNumber.ToString());
    }

    IEnumerator EnableInput(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        inputEnable = true;
    }
}

