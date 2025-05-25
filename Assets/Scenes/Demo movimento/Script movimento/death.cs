using UnityEngine;
using System.Collections;

public class death : MonoBehaviour
{
    public AudioClip hurtSound; // assegna il suono da Inspector
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Se non c'è un AudioSource, lo aggiungiamo dinamicamente
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void ReadDead1(bool isDead1)
    {
        if (isDead1)
        {
            //StartCoroutine(HandlePlayerDeath("Player1", "Player2"));
        }
    }

    public void ReadDead2(bool isDead2)
    {
        if (isDead2)
        {
            //StartCoroutine(HandlePlayerDeath("Player2", "Player1"));
        }
    }

    public void HandlePlayerDeath()
    {
        Animator anim = GetComponent<Animator>();
        if (anim == null)
        {
            Destroy(gameObject);
            var move = GetComponent<PlayerLaneMovement>();
            string winnerPlayer = "1";
            if (move != null && move.PlayerNumber == "1")
            {
                winnerPlayer = "2";
            }
            GameManager.Instance.DeclareVictory("Player " + winnerPlayer);
        }
        else
        {
            // Trigger animazione Hurt
            anim.SetTrigger("isHurt");

            // Riproduci il suono
            if (hurtSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hurtSound);
            }
        }
    }
}
