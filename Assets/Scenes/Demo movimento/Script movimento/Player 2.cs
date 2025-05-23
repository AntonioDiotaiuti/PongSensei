using UnityEngine;

public class Player2LaneMovement : MonoBehaviour
{
    [Header("Movimento Verticale")]
    public float laneOffset = 2f; 
    public int currentLane = 1;   

    [Header("Movimento Orizzontale")]
    [SerializeField] private float horizontalSpeed = 5f;
    public BoxCollider ArenaBounds;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        UpdateLanePosition();
    }

    void Update()
    {
        // Input verticale (a scatti)
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLane < 2)
        {
            currentLane++;
            UpdateLanePosition();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLane > 0)
        {
            currentLane--;
            UpdateLanePosition();
        }

        // Input orizzontale (fluido)
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }

        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
        var actualPos = transform.position;
        if (transform.position.x < ArenaBounds.bounds.min.x)
        {
            actualPos.x = ArenaBounds.bounds.min.x;
            transform.position = actualPos;
        }
        else if (transform.position.x > ArenaBounds.bounds.max.x)
        {
            actualPos.x = ArenaBounds.bounds.max.x;
            transform.position = actualPos;
        }
    }

    void UpdateLanePosition()
    {
        float newY = startPosition.y + (currentLane - 1) * laneOffset;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
