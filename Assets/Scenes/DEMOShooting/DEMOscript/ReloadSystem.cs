using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSystem : MonoBehaviour {

    public PlayerLaneMovement MovementComp;
    [Header("Impostazioni Giocatore")]
    [SerializeField] private string playerNumber = "1";
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Image reloadBar;

    private float reloadTimer = 0f;
    private bool hasAmmo = true;
    private bool isMoving = false;

    private string horizontalAxis;
    private string verticalAxis;

    private void Awake() {
        MovementComp.OnMovementUpdate += OnMovementUpdate;
    }

    private void OnDestroy() {
        MovementComp.OnMovementUpdate -= OnMovementUpdate;
    }

    void Start()
    {
        reloadBar.fillAmount = 1f;

        // Imposta i nomi degli assi in base al PlayerNumber
        horizontalAxis = "Horizontal" + playerNumber;
        verticalAxis = "Vertical" + playerNumber;
    }

    void Update()
    {
        // Verifica SOLO i movimenti del proprio player

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
                    hasAmmo = true;
                    reloadTimer = 0f;
                    reloadBar.fillAmount = 1f;
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
    }

    public bool HasAmmo()
    {
        return hasAmmo;
    }

    private void OnMovementUpdate(bool moving) {
        isMoving = moving;
        if (isMoving && !hasAmmo) {
            ResetReload();
        }
        
        Debug.Log("UPDATE -> "+isMoving);
    }
}
