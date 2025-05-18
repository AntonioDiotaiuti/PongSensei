using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Vector2 direction = Vector2.right;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            StartCoroutine(HandlePlayerDeath(collision.gameObject, "Player2"));
        }
        else if (collision.CompareTag("Player2"))
        {
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

        //Ferma il proiettile
        gameObject.SetActive(false);

        // Trigger animazione Hurt
        anim.SetTrigger("isHurt");

        //Aspetta che HurtP1 sia attiva
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("HurtP1"))
            yield return null;

        //Aspetta la durata reale di HurtP1
        yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

        //Trigger animazione Death
        anim.SetTrigger("isDeath");

        //Aspetta che DeathP1 sia attiva
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("DeathP1"))
            yield return null;

        //Aspetta la durata reale di DeathP1
        yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

        // Distruggi player
        Destroy(player);

        //Vittoria + blocco gioco
        GameManager.Instance.DeclareVictory(winner);
    }
}
