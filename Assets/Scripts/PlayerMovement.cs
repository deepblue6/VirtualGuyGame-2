using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // variables
    private Rigidbody2D rb; // referencing the Rigidbody2D component
    private Animator anim; // reference animator
    private float dirX = 0f;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f; // serialize makes the value changeable in unity
    [SerializeField] private float jumpForce = 14f; // serialize makes the value changeable in unity
    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    private bool doubleJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKey(left))
        {
            dirX = -1f; // Move left
        }
        else if (Input.GetKey(right))
        {
            dirX = 1f; // Move right
        }
        else
        {
            dirX = 0f; // No movement
        }
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (IsGrounded())
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(jump))
        {
            if (IsGrounded() || !doubleJump)
            {
                doubleJump = !IsGrounded();
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else if (Input.GetKey(jump) && rb.velocity.y > 0f)
        {
            // Increase the jump force while the jump button is held down, up to a maximum cap
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime * 0.5f, ForceMode2D.Impulse);

            // Cap the maximum jump height
            if (rb.velocity.y > jumpForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetKeyUp(jump) && rb.velocity.y > 0f)
        {
            // Reduce the upward velocity when the jump button is released mid-jump
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return hit.collider != null;
    }
}
