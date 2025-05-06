using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float activeTime = 0.2f;
    private bool isActive = false;

    private void Start() => gameObject.SetActive(false);

    public void Activate()
    {
        isActive = true;
        gameObject.SetActive(true);
        Invoke(nameof(Deactivate), activeTime);
    }

    private void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Enemy"))
        {
            // Applica danno o effetto
            Debug.Log("Nemico colpito!");
            // other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
