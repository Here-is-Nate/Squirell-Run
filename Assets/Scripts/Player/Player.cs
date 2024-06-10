using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rgdBdy2d;

    [Header("Player Basics")]
    [SerializeField] private int life;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashTime;
    private float initialMovementSpeed;
    private float lockedYPosition;
    private bool canDash;

    [Header("Invicible Data")]
    private float invicibilityTime = 1f;
    private float invicibilityTimeCount;

    [Header("Jump Controlls")]
    [SerializeField] private float jumpForce;
    private bool _isOnTheGround;
    private bool canJump = true;

     [Header("Enemy Jump Data")]
    private float enemyJumpTime = 0.2f;
    private float enemyJumpTimeCount;

    [Header("Coyote Jump")]
    private float coyoteTimer = 0.1f;
    private float coyoteCount;

    [Header("Input Keys")]
    private KeyCode KEY_RIGHT = KeyCode.D;
    private KeyCode KEY_LEFT = KeyCode.A;

    #region Properties
    [HideInInspector]
    public bool isOnTheGround {get {return _isOnTheGround;} set {_isOnTheGround = value;}}
    [HideInInspector]
    //Player States
    public bool isMoving, isJumping, isDashing, isHurting;
    [HideInInspector]
    public int movingDirection;
    #endregion

    void Start() {
        rgdBdy2d = GetComponent<Rigidbody2D>();
        movingDirection = 1;
        initialMovementSpeed = movementSpeed;
    }

    void Update() {
        GetInputs();
        CoyoteHandler();
        
        if(invicibilityTimeCount < invicibilityTime) invicibilityTimeCount += Time.deltaTime;
        if(enemyJumpTimeCount < enemyJumpTime) enemyJumpTimeCount += Time.deltaTime;

        MovePlayer();
    }

    void GetInputs() {
        if(isDashing || isHurting) return;

        if(canDash) {
            if(Input.GetKeyDown(KEY_LEFT) || Input.GetKeyDown(KEY_RIGHT)) Dash((int) Input.GetAxisRaw("Horizontal"));  
        }
        if(Input.GetKeyDown(KeyCode.Space) && canJump) Jump();
    }

    #region Movement
    void MovePlayer() {
        transform.position = new Vector2(transform.position.x + movingDirection * movementSpeed * Time.deltaTime, isDashing ? lockedYPosition : transform.position.y);
        isMoving = true;
    }

    void Jump() {
        rgdBdy2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    public void EnemyJump() {
        if(enemyJumpTimeCount >= enemyJumpTime) {
            rgdBdy2d.velocity = Vector3.zero;
            rgdBdy2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            enemyJumpTimeCount = 0;
        }
    }

    void Dash(int dir) {
        movingDirection = dir;
        isDashing = true;
        canDash = false;
        movementSpeed = movementSpeed * 2;
        lockedYPosition = transform.position.y;
        StartCoroutine(StopDash());
    }
    #endregion

    #region Handlers
    IEnumerator StopDash() {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        movementSpeed = initialMovementSpeed;
    }

    public void GetHurt(float knockForce) {
        if(invicibilityTimeCount < invicibilityTime) return;

        life--;
        invicibilityTimeCount = 0;

        isHurting = true;

        rgdBdy2d.velocity = Vector3.zero;
        rgdBdy2d.AddForce(new Vector2(knockForce * movingDirection * (-1f), knockForce), ForceMode2D.Impulse);

        if(life <= 0) Die();
    }

    void Die() {

    }

    void CoyoteHandler() {
        if(isOnTheGround) canDash = true;

        if(isOnTheGround && !canJump) {
            canJump = true;
            coyoteCount = 0;
            isJumping = false;
            isHurting = false;
            return;
        }

        if(!isOnTheGround && coyoteCount < coyoteTimer) coyoteCount += Time.deltaTime;
        if(coyoteCount >= coyoteTimer && canJump) canJump = false;
    }
    #endregion    
}