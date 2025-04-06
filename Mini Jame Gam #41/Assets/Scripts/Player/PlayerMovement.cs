using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    Rigidbody2D rb;
    PlayerStats player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0 && moveDir.y == 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
            lastVerticalVector = 0f;
        }

        if(moveDir.x == 0 && moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
            lastHorizontalVector = 0f;
        }

        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
    }
}
