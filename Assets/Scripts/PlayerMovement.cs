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
    private PlayerData playerData;

    // player movement events
    public event Action<PlayerMovement> onMove;
    public event Action<PlayerMovement> onJump;
    public event Action<PlayerMovement> onFall;
    public event Action onLand;
    public event Action onIdle;
    public event Action<bool> onFlip;

    public event Action onFinish;

    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerData = GetComponent<PlayerData>();

        onJump += jump;
        playerData.onDeath += removeMovement;
        onFinish += removeMovement;
    }
    private void OnDestroy()
    {
        onJump -= jump;
        playerData.onDeath -= removeMovement;
        onFinish -= removeMovement;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            onFlip?.Invoke(horizontal > 0);
        // player not jumping
        if (!jumped)
        {
            // play running animation when on ground
            if (horizontal != 0)
                onMove?.Invoke(this);
            // only can jump if havent jumped yet
            if (Input.GetButtonDown("Jump"))
                onJump?.Invoke(this);
            // if player is not moving at all & not jumping, trigger idle event
            if (horizontal==0)
            {
                onIdle?.Invoke();
            }
        }

        // in jumping motion
        else if (jumped)
        {
            // as y velocity starts to change to negative -> falling
            if (rb.velocity.y < 0 && rb.velocity.y > -0.25f)
                onFall?.Invoke(this);

            // if y velocity is negative and near ground -> landing
            if (rb.velocity.y < 0 && isGrounded())
            {
                onLand?.Invoke();
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

    private void removeMovement()
    {
        rb.velocity = Vector2.zero;
        Destroy(this);
    }

    // proper ground check using boxcast
    public LayerMask terrain;
    bool isGrounded()
    {
        Collider2D collider = GetComponent<CapsuleCollider2D>();
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
        if (other.gameObject.CompareTag("Finish"))
            onFinish?.Invoke();
        if (other.gameObject.CompareTag("Boundary"))
            playerData.onDeath?.Invoke();

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IObstacle>()?.onDamagePlayer(this.gameObject, collision.relativeVelocity);
    }



}
