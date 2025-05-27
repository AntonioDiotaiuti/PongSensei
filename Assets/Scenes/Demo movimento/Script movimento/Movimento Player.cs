using System.Collections;
using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    public BoxCollider2D ArenaBounds;
    public string PlayerNumber = "1";

    [Header("Movimento Verticale (a scatti)")]
    public float laneOffset = 2f;
    public int currentLane = 1;

    [Header("Movimento Orizzontale (fluido)")]
    [SerializeField] public float horizontalSpeed = 5f;

    private Vector3 startPosition;
    private bool verticalAxisInUse = false;
    private string horizontalAxisPlayer;
    private string verticalAxisPlayer;

    public bool isMoving = false;

    public bool InputAllowed = true;
    public float MovementCooldown = 0.2f;
    private Animator animator;

    public delegate void MovementEvent(bool moving);
    public MovementEvent OnMovementUpdate;

    void Start()
    {
        horizontalAxisPlayer = "DPadHorizontal" + PlayerNumber;
        verticalAxisPlayer = "DPadVertical" + PlayerNumber;
        startPosition = transform.position;
        UpdateLanePosition();

        animator = GetComponent<Animator>(); // animator
    }

    void Update()
    {
        if (InputAllowed)
        {
            HandleVerticalInput();
            HandleHorizontalInput();
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void AllowInput()
    {
        StartCoroutine(AllowInputAsync());
    }

    void HandleVerticalInput()
    {
        bool moveUp = false;
        bool moveDown = false;

        if (IsPlayerOne())
        {
            moveUp = Input.GetKeyDown(KeyCode.W);
            moveDown = Input.GetKeyDown(KeyCode.S);
        }
        else
        {
            moveUp = Input.GetKeyDown(KeyCode.UpArrow);
            moveDown = Input.GetKeyDown(KeyCode.DownArrow);
        }

        float verticalInput = Input.GetAxis(verticalAxisPlayer);

        if (!verticalAxisInUse)
        {
            if ((moveUp || verticalInput > 0.5f) && currentLane < 2)
            {
                currentLane++;
                UpdateLanePosition();
                verticalAxisInUse = true;

                // DASH 
                if (animator != null)
                {
                    animator.SetTrigger("isDashing");
                }
            }
            else if ((moveDown || verticalInput < -0.5f) && currentLane > 0)
            {
                currentLane--;
                UpdateLanePosition();
                verticalAxisInUse = true;

                // DASH
                if (animator != null)
                {
                    animator.SetTrigger("isDashing");
                }
            }
            else
            {
                OnMovementUpdate?.Invoke(false);
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

        if (IsPlayerOne())
        {
            bool pressD = Input.GetKey(KeyCode.D);
            bool pressA = Input.GetKey(KeyCode.A);

            if (pressD) horizontalInput = 1f;
            else if (pressA) horizontalInput = -1f;

        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;
            else if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
        }

        float horizontalInputPad = Input.GetAxis(horizontalAxisPlayer);

        bool usedPad = Mathf.Abs(horizontalInputPad) > 0.5f;
        isMoving = horizontalInput != 0.0f || usedPad;

        if (animator != null)
            animator.SetBool("isMoving", isMoving);

        OnMovementUpdate?.Invoke(isMoving);

        float movingToConsider = usedPad ? horizontalInputPad : horizontalInput;
        transform.Translate(Vector3.right * movingToConsider * horizontalSpeed * Time.deltaTime);

        if (ArenaBounds != null)
        {
            var actualPos = transform.position;
            if (transform.position.x > ArenaBounds.bounds.max.x)
                actualPos.x = ArenaBounds.bounds.max.x;
            else if (transform.position.x < ArenaBounds.bounds.min.x)
                actualPos.x = ArenaBounds.bounds.min.x;

            transform.position = actualPos;
        }
    }

    void UpdateLanePosition()
    {
        float newY = startPosition.y + (currentLane - 1) * laneOffset;
        OnMovementUpdate?.Invoke(true);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private bool IsPlayerOne()
    {
        return PlayerNumber == "1";
    }

    private IEnumerator AllowInputAsync()
    {
        yield return new WaitForSeconds(MovementCooldown);

        InputAllowed = true;
    }
}
