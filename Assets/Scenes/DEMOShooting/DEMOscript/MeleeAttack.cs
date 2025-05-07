using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float activeTime = 0.2f;
    public string attackerTag; // "Player1" o "Player2"

    private bool isActive = false;

    private void Start() => gameObject.SetActive(false);

    public void Activate()
    {
        isActive = true;
        gameObject.SetActive(true);
        Invoke(nameof(Deactivate), activeTime);
    }

    private void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger colpito da: " + other.name + ", tag: " + other.tag);

        if (!isActive) return;

        if ((attackerTag == "Player1" && other.CompareTag("Player2")) ||
            (attackerTag == "Player2" && other.CompareTag("Player1")))
        {
            Debug.Log(attackerTag + " ha colpito e ucciso " + other.tag);
            GameManager.Instance.DeclareVictory(attackerTag);
        }
    }
}

