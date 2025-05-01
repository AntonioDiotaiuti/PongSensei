using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    [Header("Movimento Verticale")]
    public float laneOffset = 2f; // Distanza tra le corsie
    public int currentLane = 1;   // 0 = basso, 1 = centro, 2 = alto

    [Header("Movimento Orizzontale")]
    [SerializeField] private float horizontalSpeed = 5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        UpdateLanePosition();
    }

    void Update()
    {
        // Input verticale (a scatti)
        if (Input.GetKeyDown(KeyCode.W) && currentLane < 2)
        {
            currentLane++;
            UpdateLanePosition();
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentLane > 0)
        {
            currentLane--;
            UpdateLanePosition();
        }

        // Input orizzontale (fluido)
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }

        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
    }

    void UpdateLanePosition()
    {
        float newY = startPosition.y + (currentLane - 1) * laneOffset;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
