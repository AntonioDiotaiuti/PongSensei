using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    public BoxCollider ArenaBounds;
    public string PlayerNumber = "1";
    [Header("Movimento Verticale (a scatti)")]
    public float laneOffset = 2f;
    public int currentLane = 1;

    [Header("Movimento Orizzontale (fluido)")]
    [SerializeField] private float horizontalSpeed = 5f;

    private Vector3 startPosition;
    private bool verticalAxisInUse = false;
    private string horizontalAxisPlayer;
    private string verticalAxisPlayer;

    void Start()
    {
        horizontalAxisPlayer = "DPadHorizontal" + PlayerNumber;
        verticalAxisPlayer = "DPadVertical" + PlayerNumber;
        startPosition = transform.position;
        UpdateLanePosition();
    }

    void Update()
    {
        HandleVerticalInput();
        HandleHorizontalInput();
    }

    void HandleVerticalInput()
    {
        bool moveUp = false;
        bool moveDown = false;
        if (IsPlayerOne())
        {
            moveUp = Input.GetKeyDown(KeyCode.W);
            moveDown = Input.GetKeyDown(KeyCode.S);
        } else
        {
            moveUp = Input.GetKeyDown(KeyCode.UpArrow);
            moveDown = Input.GetKeyDown(KeyCode.DownArrow);
        }
        

        float verticalInput = Input.GetAxis(verticalAxisPlayer); // SOLO D-PAD ↑/↓

        if (!verticalAxisInUse)
        {
            if ((moveUp || verticalInput > 0.5f) && currentLane < 2)
            {
                currentLane++;
                UpdateLanePosition();
                verticalAxisInUse = true;
            }
            else if ((moveDown || verticalInput < -0.5f) && currentLane > 0)
            {
                currentLane--;
                UpdateLanePosition();
                verticalAxisInUse = true;
            }
        }

        if (Mathf.Abs(verticalInput) < 0.3f)
        {
            verticalAxisInUse = false;
        }
    }

    void HandleHorizontalInput()
    {
        float horizontalInput = 0f;

        // Tastiera
        if (IsPlayerOne())
        {
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
            else if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        } else
        {
            if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;
            else if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
        }
        

        // Freccette controller (← / →)
        horizontalInput += Input.GetAxis(horizontalAxisPlayer);
        if (horizontalInput != 0.0f)
        {
            Debug.Log("INPUT -> "+horizontalInput);
        } else
        {
            Debug.Log("INPUT -> none");
        }

        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
        if (ArenaBounds != null)
        {
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
    }

    void UpdateLanePosition()
    {
        float newY = startPosition.y + (currentLane - 1) * laneOffset;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private bool IsPlayerOne()
    {
        return PlayerNumber == "1";
    }
}
