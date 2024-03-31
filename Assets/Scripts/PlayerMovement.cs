using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            jumpSound.Play();
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (player.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (player.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state);
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
