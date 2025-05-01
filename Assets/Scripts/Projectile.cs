using UnityEngine;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    private int direction = 1;

    public void SetDirection(int dir)
    {
        direction = dir;
        transform.localScale = new Vector3(dir, 1, 1); // Per girare visivamente il proiettile
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

 
}
