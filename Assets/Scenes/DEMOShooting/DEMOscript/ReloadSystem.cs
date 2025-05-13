using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSystem : MonoBehaviour
{
    public PlayerLaneMovement MovementComp;
    public static event Action<string> OnPlayerShot; // Evento per segnalare il colpo sparato

    [Header("Impostazioni Giocatore")]
    [SerializeField] private string playerNumber = "1";
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Image reloadBar;

    private float reloadTimer = 0f;
    private bool hasAmmo = true;
    private bool isMoving = false;

    private string horizontalAxis;
    private string verticalAxis;

    private void Awake()
    {
        MovementComp.OnMovementUpdate += OnMovementUpdate;
    }

    private void OnDestroy()
    {
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
                    Reload();
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

        // Notifica che il giocatore ha sparato
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
            Reload();
            Destroy(reloadItem.gameObject);
        }
    }
}

