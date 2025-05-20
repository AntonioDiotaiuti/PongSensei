using UnityEngine;
using System.Collections;

public class death : MonoBehaviour
{
    
    public void ReadDead1(bool isDead1)
    {
        if (isDead1 != false)
        {
            //StartCoroutine(HandlePlayerDeath("Player1", "Player2"));
        }
    }

    public void ReadDead2(bool isDead2)
    {
        if (isDead2 != false)
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
            GameManager.Instance.DeclareVictory("Player "+ winnerPlayer);
        } else
        {
            // Trigger animazione Hurt
            anim.SetTrigger("isHurt");
        }
    }
}

