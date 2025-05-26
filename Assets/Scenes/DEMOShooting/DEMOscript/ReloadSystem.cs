using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSystem : MonoBehaviour
{
    public PlayerLaneMovement MovementComp;
    public static event Action<string> OnPlayerShot;

    [Header("Impostazioni Giocatore")]
    [SerializeField] private string playerNumber = "1";
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Image reloadBar;

    [Header("Audio")]
    public AudioClip reloadSound;
    private AudioSource audioSource;

    private float reloadTimer = 0f;
    private bool hasAmmo = true;
    private bool isMoving = false;

    private string horizontalAxis;
    private string verticalAxis;

    private void Awake()
    {
        if (MovementComp != null)
            MovementComp.OnMovementUpdate += OnMovementUpdate;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (MovementComp != null)
            MovementComp.OnMovementUpdate -= OnMovementUpdate;
    }

    void Start()
    {
        reloadBar.fillAmount = 1f;

        horizontalAxis = "Horizontal" + playerNumber;
        verticalAxis = "Vertical" + playerNumber;
    }

    void Update()
    {
        if (!hasAmmo)
        {
            if (isMoving)
            {
                ResetReload();
            }
            else
            {
                reloadTimer += Time.deltaTime;
                reloadBar.fillAmount = reloadTimer / reloadTime;

                if (reloadTimer >= reloadTime)
                {
                    Reload(); // Il suono viene gestito dentro Reload()
                }
            }
        }
    }

    private void ResetReload()
    {
        reloadTimer = 0f;
        reloadBar.fillAmount = 0f;
    }

    public void ConsumeAmmo()
    {
        hasAmmo = false;
        reloadBar.fillAmount = 0f;
        OnPlayerShot?.Invoke(playerNumber);
    }

    public bool HasAmmo()
    {
        return hasAmmo;
    }

    private void Reload()
    {
        hasAmmo = true;
        reloadTimer = 0f;
        reloadBar.fillAmount = 1f;

        // Suono del reload
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
    }

    private void OnMovementUpdate(bool moving)
    {
        isMoving = moving;
        if (isMoving && !hasAmmo)
        {
            ResetReload();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ReloadItem reloadItem = other.GetComponent<ReloadItem>();
        if (reloadItem != null && !hasAmmo)
        {
            Reload(); // Anche qui il suono parte automaticamente
            Destroy(reloadItem.gameObject);
        }
    }
}
