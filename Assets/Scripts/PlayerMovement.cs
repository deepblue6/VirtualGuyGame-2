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
    private enum MovementState { idle, running, jumping, falling}
    private MovementState state = MovementState.idle;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    
    


    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }




    // Update is called once per frame
    private void Update() // void means it does not return a value, it just runs code/commands
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level 1.0.2");
        }

        if (Input.GetKey(left))
        {
            dirX = -1;
        }
        else if (Input.GetKey(right))
        {
            dirX = 1;
        }
        else
        {
            dirX = 0;
        }
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if ((Input.GetKeyDown(jump) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

            // uses vector2 because it is a 2D game, multiplies by dirX.
            // If it is negative, it goes left, if positive, go right.



            UpdateAnimationState(); // CLEAN CODE!!!!!!!!!!!
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
    } // clean code

    private bool IsGrounded()
{
    // BoxCast returns a RaycastHit2D object that can be used to check if it hit something
    // You can use a boolean expression to check if it hit something and return the result
    RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    return hit.collider != null; // Return true if the boxcast hit something (ground), false otherwise
}

}