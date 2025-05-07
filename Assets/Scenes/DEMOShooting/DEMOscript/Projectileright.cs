using UnityEngine;

public class Projectileright : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2"))
        {
            GameManagerino.Instance.DeclareVictory("Player1");
            Destroy(other.gameObject);  
        }

        Destroy(gameObject);  
    }
}


