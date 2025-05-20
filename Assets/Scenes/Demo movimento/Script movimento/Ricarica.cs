using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Vector2 direction = Vector2.right;

    // morte
    public bool Death = false;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Death = true; // attiva booleana
            StartCoroutine(HandlePlayerDeath(collision.gameObject, "Player2"));
        }
        else if (collision.CompareTag("Player2"))
        {
            Death = true;
            StartCoroutine(HandlePlayerDeath(collision.gameObject, "Player1"));
        }
    }

    IEnumerator HandlePlayerDeath(GameObject player, string winner)
    {
        Animator anim = player.GetComponent<Animator>();
        if (anim == null)
        {
            Destroy(player);
            GameManager.Instance.DeclareVictory(winner);
            yield break;
        }

        // Ferma il proiettile
        gameObject.SetActive(false);

        // Trigger animazione Hurt
        anim.SetTrigger("isHurt");

        
    }
}
