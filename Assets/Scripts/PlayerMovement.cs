using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 7.5f;
    [SerializeField] float climbingSpeed = 1f;
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] private WallJumping wallJumping;

    public Rigidbody2D myRigidbody;
    public float jumpSpeed = 5f;

    private Vector2 _moveInput;
    private Animator _myAnimator;
    private CapsuleCollider2D _myBodyCollider;
    private BoxCollider2D _myFeetCollider;
    private SpriteRenderer _mySpriteRenderer;
    private float _gravityScaleAtStart;
    private bool _isAlive = true;
    [SerializeField] private bool _onJump = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myBodyCollider = GetComponent<CapsuleCollider2D>();
        _myFeetCollider = GetComponentInChildren<BoxCollider2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        if (!_onJump) Run();
        FlipSprite();
        ClimbLadder();
        Die();
        IfOnJump();
    }


    void OnMove(InputValue value)
    {
        if (!_isAlive)
        {
            return;
        }

        _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!_isAlive)
        {
            return;
        }

        if (value.isPressed)
        {
            if (_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
            }

            if (_myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                myRigidbody.linearVelocityY = 0;
                if (myRigidbody.linearVelocity.x < 0f)
                {
                    myRigidbody.linearVelocityX = 0;
                    myRigidbody.linearVelocity += new Vector2(-15f, 17f);
                }
                else
                {
                    myRigidbody.linearVelocityX = 0;
                    myRigidbody.linearVelocity += new Vector2(15f, 17f);
                }
            }
        }
    }

    private void IfOnJump()
    {
        if (_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _onJump = false;
        }
        else
        {
            _onJump = true;
        }
    }

    // void OnFire(InputValue value)
    // {
    //     if (!_isAlive)
    //     {
    //         return;
    //     }
    //
    //     Instantiate(arrow, bow.position, transform.rotation);
    // }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        _myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = _gravityScaleAtStart;
            _myAnimator.SetBool("isClimbing", false);
            return;
        }

        myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, _moveInput.y * climbingSpeed);
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = MathF.Abs(_moveInput.y) > Mathf.Epsilon;
        _myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die()
    {
        if (_myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            _isAlive = false;
            _myAnimator.SetTrigger("Dying");
            myRigidbody.linearVelocity = deathKick;
            _mySpriteRenderer.color = Color.gray;
            FindFirstObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }
}