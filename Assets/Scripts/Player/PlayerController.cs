using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    #region Tuneable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    #endregion
    #region Getters and Setters
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value >= maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                _lives = 0;
                //go to game over
            }
            else if (_lives > value)
            {
                _lives = value;
                //respawn player
            }
            else
            {
                _lives = value;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }
    #endregion
    #region Ground Check Parameters
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    #endregion
    #region Tunable Parameters
    public int maxJumps = 2;
    public int maxLives = 9;
    #endregion
    #region Components References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(CalculateGroundCheckPos(), groundCheckRadius, groundLayer);
        }


        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

        }
        if (moveX != 0f)
        { 
            SpriteFlip(moveX);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttack");
        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetTrigger("IsjumpAttack");
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));

    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);

}
