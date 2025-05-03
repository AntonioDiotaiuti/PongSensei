using UnityEngine;

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
            GameManager.Instance.DeclareVictory("Player2");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player2"))
        {
            GameManager.Instance.DeclareVictory("Player1");
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
