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
        // Puoi personalizzare: se colpisce qualcosa, distruggilo
        Destroy(gameObject);
    }
}
