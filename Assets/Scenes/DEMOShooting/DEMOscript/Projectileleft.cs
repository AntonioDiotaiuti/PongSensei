using UnityEngine;

public class Projectileleft : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            GameManagerino.Instance.DeclareVictory("Player2");
            Destroy(other.gameObject);  
        }

        Destroy(gameObject);  
    }
}


