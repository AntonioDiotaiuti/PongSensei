using UnityEngine;

public class FlipPlayer : MonoBehaviour
{

    public SpriteRenderer Player1;
    public SpriteRenderer Player2;
    public BoxCollider2D ColliderParry1;
    public BoxCollider2D ColliderParry2;
    public GameObject FirePoint1;
    public GameObject FirePoint2;
    public enum PlayerSide {  Left, Right };
    public PlayerSide Side1;
    public PlayerSide Side2;

    private PlayerShooting shooting1;
    private PlayerShooting shooting2;

    private void Awake()
    {
        shooting1 = Player1.GetComponent<PlayerShooting>();
        shooting2 = Player2.GetComponent<PlayerShooting>();
    }

    void Update()
    {
        CheckPositionWithOther();
    }

    private void CheckPositionWithOther()
    {
        if (Player1 != null && Player2 != null)
        {
            Debug.Log("here");
            float player1X = Player1.transform.position.x;
            float player2X = Player2.transform.position.x;

            if (player1X < player2X)
            {
                Debug.Log("here - 2");
                Player1.flipX = false;
                Side1 = PlayerSide.Left;
                var offset1 = ColliderParry1.offset;
                offset1.x = Mathf.Abs(offset1.x);
                ColliderParry1.offset = offset1;
                var pos1 = FirePoint1.transform.localPosition;
                pos1.x = Mathf.Abs(pos1.x);
                FirePoint1.transform.eulerAngles = Vector3.zero;
                FirePoint1.transform.localPosition = pos1;

                Player2.flipX = true;
                Side2 = PlayerSide.Right;
                var offset2 = ColliderParry2.offset;
                offset2.x = Mathf.Abs(offset2.x) * -1.0f;
                ColliderParry2.offset = offset2;
                var pos2 = FirePoint2.transform.localPosition;
                pos2.x = Mathf.Abs(pos2.x) * -1.0f;
                FirePoint2.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                FirePoint2.transform.localPosition = pos2;
            } else
            {
                Debug.Log("here - 3");
                Player1.flipX = true;
                Side1 = PlayerSide.Right;
                var offset1 = ColliderParry1.offset;
                offset1.x = Mathf.Abs(offset1.x) * -1.0f;
                ColliderParry1.offset = offset1;
                var pos1 = FirePoint1.transform.position;
                pos1.x = Mathf.Abs(pos1.x) * -1.0f;
                FirePoint1.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                FirePoint1.transform.position = pos1;

                Player2.flipX = false;
                Side2 = PlayerSide.Left;
                var offset2 = ColliderParry2.offset;
                offset2.x = Mathf.Abs(offset2.x);
                ColliderParry2.offset = offset2;
                var pos2 = FirePoint2.transform.position;
                pos2.x = Mathf.Abs(pos2.x);
                FirePoint2.transform.eulerAngles = Vector3.zero;
                FirePoint2.transform.position = pos2;
            }
        }
    }
}
