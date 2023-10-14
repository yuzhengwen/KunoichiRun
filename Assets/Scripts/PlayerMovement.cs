using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float playerVelocity = 5;
    [SerializeField]
    private float jumpVelocity = 3;
    private float horizontal = 0;

    private Rigidbody2D rb;

    // player movement events
    public event Action<PlayerMovement> onJump;
    public event Action<PlayerMovement> onLand;
    public event Action<PlayerMovement> onMove;
    public event Action<PlayerMovement> onIdle;

    private bool jumped = false;
    private bool startFalling = false;

    public enum state
    {
        Grounded, Jumping, Running
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        onJump += jump;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (!jumped && horizontal != 0) 
            onMove?.Invoke(this);
        if (!jumped && Input.GetButtonDown("Jump"))
            onJump?.Invoke(this);

        // check jump landed, trigger on land event
        if (rb.velocity.y == 0 && jumped)
        {
            // first time v=0 at the top
            if (!startFalling)
                startFalling = true;
            // 2nd time v=0 when landed
            else
            {
                onLand?.Invoke(this);
                startFalling = false;
                jumped = false;
            }
        }

        // if player is not moving, trigger idle event
        if (!jumped && rb.velocity.magnitude == 0)
        {
            onIdle?.Invoke(this);
        }
    }
    void FixedUpdate()
    {
        calculateMovement();
    }

    private void calculateMovement()
    {
        rb.velocity = new Vector2(horizontal * playerVelocity, rb.velocity.y);
    }

    private void jump(PlayerMovement playerMovement)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpVelocity);
        jumped = true;
    }

    // proper ground check using raycasts, to be used later
    public LayerMask groundLayer;
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    public float getFacingDirection()
    {
        return horizontal;
    }
}
