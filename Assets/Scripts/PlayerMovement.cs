using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public event Action<PlayerMovement> onFall;
    public event Action<PlayerMovement> onLand;
    public event Action<PlayerMovement> onMove;
    public event Action<PlayerMovement> onIdle;

    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        onJump += jump;
    }
    private void OnDestroy()
    {
        onJump -= jump;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!jumped)
        {
            if (horizontal != 0)
                onMove?.Invoke(this);
            if (Input.GetButtonDown("Jump"))
                onJump?.Invoke(this);
            // if player is not moving at all, trigger idle event
            if (rb.velocity.magnitude ==0)
            {
                onIdle?.Invoke(this);
            }
        }

        // in jumping motion
        else if (jumped)
        {
            // as y velocity changes to negative -> falling
            if (rb.velocity.y < 0 && rb.velocity.y > -0.25f)
                onFall?.Invoke(this);

            // if y velocity is negative and near ground -> landing
            if (rb.velocity.y < 0 && isGrounded())
            {
                onLand?.Invoke(this);
                jumped = false;
            }
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

    // proper ground check using boxcast
    public LayerMask terrain;
    bool isGrounded()
    {
        Collider2D collider = GetComponent<PolygonCollider2D>();
        // create a box cast collider slightly below player to check for ground
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, terrain);
    }
    public float getFacingDirection()
    {
        return horizontal;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<ICollectible>()?.onPlayerCollect(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IObstacle>()?.onDamagePlayer(this.gameObject);
    }
}
