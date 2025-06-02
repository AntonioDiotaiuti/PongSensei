using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float CooldownInput = 0.3f;

    [SerializeField] public string playerNumber = "1";
    [SerializeField] private KeyCode keyboardKey = KeyCode.R;

    public bool inputEnable = true;
    private bool triggerHeld = false;
    private string fireAxis;
    private Vector3 shootDirection;

    private ReloadSystem reloadSystem;
    private AudioSource audioSource;
    private Animator animator;
    public bool MenuActive;

    public AudioClip shootSound;
    private void Awake()
    {
        var Menu = GameObject.FindAnyObjectByType<ControlSchemeManager>();
        if (Menu != null)
        {
            Menu.OnSetMenu += GetMenu;
        }
    }
    public void GetMenu(bool Menu)
    {
        Menu = MenuActive;

    }

    void Start()
    {
        reloadSystem = GetComponent<ReloadSystem>();
        animator = GetComponent<Animator>();

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

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!inputEnable || Pausamenu.GameisPaused) return;

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
       
        if (bulletComp != null && !MenuActive) 
        {
            bulletComp.direction = firePoint.right;

        }

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        reloadSystem.ConsumeAmmo();

        if (animator != null)
        {
            animator.SetBool("isThrowing", true);
            StartCoroutine(ResetThrowAnimation());
        }
    }

    IEnumerator ResetThrowAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        if (animator != null)
        {
            animator.SetBool("isThrowing", false);
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        string winnercolor = playerNumber == "1" ? "Blue" : "Red";
        GameManager.Instance.DeclareVictory(winnercolor);
    }

    IEnumerator EnableInput(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        inputEnable = true;
    }
}

