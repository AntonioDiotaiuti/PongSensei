using UnityEngine;

public class Projectileleft : MonoBehaviour
{
    [SerializeField] public float speed = 10f;




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
}
