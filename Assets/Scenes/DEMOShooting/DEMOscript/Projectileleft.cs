using UnityEngine;

public class Projectileleft : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Distrugge il proiettile dopo 'lifetime' secondi
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); // Muove il proiettile verso sinistra
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se il proiettile colpisce Player1
        if (other.CompareTag("Player1"))
        {
            // Dichiarazione della vittoria per Player2 (proiettile che va verso sinistra)
            GameManagerino.Instance.DeclareVictory("Player2");
            Destroy(other.gameObject); // Distrugge Player1
        }

        // Distrugge il proiettile dopo aver colpito Player1
        Destroy(gameObject);
    }

    // Aggiungiamo una funzione per prevenire la distruzione immediata del proiettile 
    // quando è in contatto con il proprio collider o con altri oggetti
    void OnCollisionEnter(Collision collision)
    {
        // Ignora collisioni con oggetti che non sono Player1
        if (collision.collider.CompareTag("Player1"))
        {
            // Qui puoi eseguire ulteriori logiche se necessario, come l'applicazione di danni
            Destroy(collision.gameObject); // Distrugge Player1
            Destroy(gameObject); // Distrugge il proiettile
        }
    }
}


