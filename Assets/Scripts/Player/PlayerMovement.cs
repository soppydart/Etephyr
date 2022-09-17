using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float dashPowerX = 2f;
    [SerializeField] float dashPowerY = 2f;
    [SerializeField] float dashTime = 2f;
    [SerializeField] float dashCooldown = 3f;
    [SerializeField] GameObject dashEffect;
    // [SerializeField] float distanceBetweenImages = 0.1f;
    [SerializeField] GameObject attackPoint;
    float lastImageExposition;
    int direction;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    bool isTouchingGround = true;
    bool isTouchingWall = false;
    public bool isPlayerMovementAllowed = true;
    public bool isPlayerJumpingAllowed = true;
    SpriteRenderer spriteRenderer;
    public bool playerHasMovedOnce = false;
    [SerializeField] float coyoteTime = 0.2f;
    float coyoteTimeCounter = 0;
    [SerializeField] float jumpBufferTime = 2f;
    [SerializeField] float jumpBufferCounter;
    [SerializeField] float wallSlidingSpeed = 1f;
    float distanceBetweenPlayerAndAttackPoint;
    [SerializeField] Slider dashSlider;
    PauseMenu pauseMenu;
    int dashCount = 2;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        distanceBetweenPlayerAndAttackPoint = transform.position.x - attackPoint.transform.position.x;
        dashSlider.value = 3;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }
    void Update()
    {
        Run();
        FlipSprite();
        IsTouchingGround();
        IsTouchingWall();
        Jump();
        // IsFalling();
        JumpAnimation();
        CoyoteTime();
        WallTouchCheck();
        WallSlide();
        IsTouchingTraps();
        ReplenishDashBar();
        AllowDash();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    bool hasJumped = false;
    public bool hasJumpedOnce = false;
    void OnJump(InputValue value)
    {
        if (isDashing || !isPlayerMovementAllowed)
            return;
        if (value.isPressed)
        {
            jumpBufferCounter = jumpBufferTime;
            if (isWallSliding)
            {
                // if (!spriteRenderer.flipX)
                // {
                //     myRigidbody.velocity = new Vector2(0, jumpSpeed * 1.25f);
                // }
                // else
                // {
                //     myRigidbody.velocity = new Vector2(0, jumpSpeed * 1.25f);
                // }
                if (moveInput.x == 0f)
                {
                    myRigidbody.velocity = new Vector2(0, jumpSpeed * 1.25f);
                }
                else
                {
                    Debug.Log("gae");
                    myRigidbody.velocity = new Vector2(jumpSpeed * 1.25f * Mathf.Sign(moveInput.x), jumpSpeed * 1.25f);
                }
                hasJumped = true;
                jumpBufferCounter = 0;
                hasReachedMidAir = true;
            }
        }
    }
    void Jump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0)
        {
            hasJumped = true;
            hasJumpedOnce = true;
            if (isPlayerJumpingAllowed)
                myRigidbody.velocity = new Vector2(0f, jumpSpeed);
            jumpBufferCounter = 0;
        }
    }
    [SerializeField] bool dashAllowed = true;
    bool isDashing = false;
    public bool isDodging = false;
    bool replenishDashBar = false;
    void OnDodge()
    {
        if (isDodging)
            return;
        myAnimator.SetTrigger("Dodge");
        isDodging = true;
        StartCoroutine(DodgeAfterImage());
        isPlayerMovementAllowed = false;
        float horizontalInput = moveInput.x;
        // if (spriteRenderer.flipX)
        //     myRigidbody.velocity = new Vector2(-1 * dodgeSpeed, 0f);
        // else
        //     myRigidbody.velocity = new Vector2(dodgeSpeed, 0f);

    }
    void OnDash()
    {
        if (!dashAllowed)
            return;
        // if (dashCount == 0)
        //     return;
        if (moveInput.x == 0 && moveInput.y == 0)
            return;
        dashAllowed = false;
        myAnimator.SetBool("isDashing", true);
        StartCoroutine(Dash());
    }
    [SerializeField] float verticalHangVelocity = 7f;
    IEnumerator Dash()
    {
        dashAllowed = false;
        float horizontalInput = moveInput.x;
        isDashing = true;
        bool isVerticalDash = false;
        // dashSlider.value = 1;
        if (horizontalInput != 0)
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * dashPowerX, moveInput.y * dashPowerY);
        else
        {
            // FindObjectOfType<CameraShake>().GetComponent<CameraShake>().Shake();
            bool originalFlip = spriteRenderer.flipX;
            myRigidbody.velocity = new Vector2(0f, moveInput.y * dashPowerY * 0.75f);
            isVerticalDash = true;
            spriteRenderer.flipX = originalFlip;
        }
        dashSlider.value -= 1;
        if (dashSlider.value < 0)
            dashSlider.value = 0;
        StartCoroutine(DashAfterImage());
        yield return new WaitForSeconds(dashTime);
        if (!isVerticalDash && !(moveInput.y == 0 && moveInput.x != 0))
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, verticalHangVelocity);
        // DecreaseVerticalVelocity(myRigidbody.velocity.y);
        replenishDashBar = true;
        isDashing = false;
        myAnimator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown + 0.0f);
        dashAllowed = true;
        // dashCount = 2;
    }
    void AllowDash()
    {
        if (dashSlider.value > 1)
        {
            dashAllowed = true;
            // Debug.Log(dashSlider.value);
        }
        else
            dashAllowed = false;
    }
    // void DecreaseVerticalVelocity(float velocity)
    // {
    //     if (velocity > 0)
    //     {
    //         velocity -= Time.deltaTime;
    //         myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, velocity);
    //     }
    // }
    IEnumerator DashAfterImage()
    {
        myAnimator.SetBool("isDashing", true);
        GetComponent<Ghost>().makeGhost = true;
        yield return new WaitForSeconds(1f);
        GetComponent<Ghost>().makeGhost = false;
        myAnimator.SetBool("isDashing", false);
    }
    IEnumerator DodgeAfterImage()
    {
        GetComponent<Ghost>().makeGhost = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Ghost>().makeGhost = false;
    }
    void ReplenishDashBar()
    {
        if (replenishDashBar && dashSlider.value <= 2)
        {
            dashSlider.value += Time.deltaTime;
        }
        else
        {
            replenishDashBar = false;
        }
    }
    bool hasReachedMidAir = false;
    void JumpAnimation()
    {
        jumpBufferCounter -= Time.deltaTime;
        if (hasJumped && !isTouchingGround && myRigidbody.velocity.y > 0)
        {
            coyoteTimeCounter = 0;
            myAnimator.SetBool("isGoingUp", true);
        }
        else if (hasJumped && !isTouchingGround && myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("isGoingUp", false);
            myAnimator.SetBool("isGoingDown", true);
            hasReachedMidAir = true;
        }
        else if (hasJumped && isTouchingGround && hasReachedMidAir)
        {
            myAnimator.SetBool("isGoingDown", false);
            hasJumped = false;
            hasReachedMidAir = false;
        }
    }
    // void IsFalling()
    // {
    //     if (myRigidbody.velocity.y < 0)
    //     {
    //         myAnimator.SetBool("isGoingUp", false);
    //         myAnimator.SetBool("isGoingDown", true);
    //     }
    //     else if (myRigidbody.velocity.y == 0 && isTouchingGround)
    //     {
    //         myAnimator.SetBool("isGoingDown", false);
    //         hasJumped = false;
    //         hasReachedMidAir = false;
    //     }
    // }
    void Run()
    {
        if (isDashing || !isPlayerMovementAllowed)
            return;
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed)
            playerHasMovedOnce = true;
    }
    void FlipSprite()
    {
        if (!isPlayerMovementAllowed)
            return;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (myRigidbody.velocity.x > Mathf.Epsilon)
        {
            spriteRenderer.flipX = false;
            attackPoint.transform.position = new Vector2
            (transform.position.x + Mathf.Abs(distanceBetweenPlayerAndAttackPoint),
            attackPoint.transform.position.y);
        }
        if (myRigidbody.velocity.x < -Mathf.Epsilon)
        {
            spriteRenderer.flipX = true;
            attackPoint.transform.position = new Vector2
            (transform.position.x - Mathf.Abs(distanceBetweenPlayerAndAttackPoint),
            attackPoint.transform.position.y);
        }
    }
    void IsTouchingGround()
    {
        isTouchingGround = myFeetCollider.IsTouchingLayers
        (LayerMask.GetMask("Ground", "MovingPlatform", "BreakingPlatform"));
    }
    [SerializeField] Transform frontCheck;
    [SerializeField] float checkRadius = 1f;
    [SerializeField] LayerMask Wall;
    void IsTouchingWall()
    {
        isTouchingWall = Physics2D.OverlapCircle(frontCheck.position, checkRadius, Wall);
    }
    void IsTouchingTraps()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Traps", "FallingPlatforms")) ||
        myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Traps", "FallingPlatforms")))
        {
            Debug.Log("Touched");
            GetComponent<PlayerCombat>().TouchedTraps();
        }
    }
    public void StopMoving()
    {
        isPlayerMovementAllowed = false;
        myRigidbody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("isRunning", false);
        dashAllowed = false;
        GetComponent<PlayerCombat>().attackAllowed = false;
    }
    public void StartMoving()
    {
        isPlayerMovementAllowed = true;
        isPlayerJumpingAllowed = true;
        dashAllowed = true;
        GetComponent<PlayerCombat>().attackAllowed = true;
    }
    void CoyoteTime()
    {
        if (isTouchingGround)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }
    [SerializeField] bool isWallSliding = false;
    void WallTouchCheck()
    {
        if (!isTouchingGround && isTouchingWall && myRigidbody.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    void WallSlide()
    {
        if (isWallSliding)
        {
            myAnimator.SetBool("isWallSliding", true);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -wallSlidingSpeed);
        }
        else
            myAnimator.SetBool("isWallSliding", false);
    }
    void OnAttack1()
    {
        myAnimator.SetTrigger("Attack1");
    }
    // IEnumerator SlideDown()
    // {
    //     float originalGravityScale = myRigidbody.gravityScale;
    //     myRigidbody.gravityScale = 0;
    //     myRigidbody.velocity = new Vector2(0, -wallSlidingSpeed);
    //     if (jumpBufferCounter == jumpBufferTime)
    //     {
    //         if (!spriteRenderer.flipX)
    //         {
    //             hasJumped = true;
    //             myRigidbody.velocity = new Vector2(-jumpSpeed, jumpSpeed);
    //             jumpBufferCounter = 0;
    //         }
    //         else
    //         {
    //             hasJumped = true;
    //             myRigidbody.velocity = new Vector2(jumpSpeed, jumpSpeed);
    //             jumpBufferCounter = 0;
    //         }
    //     }
    //     yield return new WaitForSeconds(wallSlidingTime);
    //     hasJumped = true;
    //     isSliding = false;
    //     myRigidbody.gravityScale = originalGravityScale;
    // }
    void OnPause()
    {
        if (PauseMenu.isPaused)
            pauseMenu.ResumeGame();
        else
            pauseMenu.PauseGame();
    }
}