using UnityEngine;

public class Projectileright : MonoBehaviour
{
    [SerializeField] public float speed = 10f;


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2"))
        {
            GameManagerino.Instance.DeclareVictory("Player1");
            Destroy(other.gameObject);  // distrugge Player2
        }

        Destroy(gameObject);  // distrugge il proiettile
    }
}


