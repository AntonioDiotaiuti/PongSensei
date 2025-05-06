using UnityEngine;

public class ParryManager : MonoBehaviour
{
    public KeyCode inputKey; // assegnato in Start() in base al player
    public float parryRange = 2f;
    public LayerMask projectileLayer;
    public Transform parryCheckOrigin;
    public MeleeAttack meleeAttack;

    private void Start()
    {
        // Imposta il tasto giusto in base al tag
        if (gameObject.CompareTag("Player1")) inputKey = KeyCode.Mouse0;
        else if (gameObject.CompareTag("Player2")) inputKey = KeyCode.Mouse1;

        meleeAttack.attackerTag = gameObject.tag; // "Player1" o "Player2"
    }

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            if (IsProjectileInFront())
            {
                Debug.Log(gameObject.tag + " ha fatto parry!");
                ParryProjectile();
            }
            else
            {
                Debug.Log(gameObject.tag + " ha fatto attacco melee");
                meleeAttack.Activate();
            }
        }
    }

    private bool IsProjectileInFront()
    {
        Ray ray = new Ray(parryCheckOrigin.position, parryCheckOrigin.forward);
        return Physics.Raycast(ray, parryRange, projectileLayer);
    }

    private void ParryProjectile()
    {
        Ray ray = new Ray(parryCheckOrigin.position, parryCheckOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, parryRange, projectileLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 reflectedDir = transform.forward;
                rb.linearVelocity = reflectedDir * rb.linearVelocity.magnitude * 1.5f;
            }
        }
    }
}


