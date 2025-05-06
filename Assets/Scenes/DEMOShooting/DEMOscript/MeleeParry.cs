using UnityEngine;

public class MeleeParry : MonoBehaviour
{
    public float activeTime = 0.2f;
    public float parrySpeedMultiplier = 1.5f;

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

        if (other.CompareTag("Proiettile"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 reflectDirection = transform.forward.normalized;
                rb.linearVelocity = reflectDirection * rb.linearVelocity.magnitude * parrySpeedMultiplier;
                other.tag = "ParriedProjectile";
                Debug.Log("Parry riuscito!");
            }
        }
    }
}

