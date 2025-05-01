using UnityEngine;

public class Projectileleft : MonoBehaviour
{
    [SerializeField]public float speed = 10f;
    [SerializeField]public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
