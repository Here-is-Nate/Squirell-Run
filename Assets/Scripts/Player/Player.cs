using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rgdBdy2d;

    [Header("Speed Controlls")]
    [SerializeField] private float movementSpeed;

    [Header("Jump Controlls")]
    [SerializeField] private float jumpForce;
    private bool _isOnTheGround;
    private bool canJump = true;

    [Header("Coyote Jump")]
    private float coyoteTimer = 0.1f;
    private float coyoteCount;


    [Header("Inputs")]
    private float movementInput;

    #region Properties
    [HideInInspector]
    public bool isOnTheGround {get {return _isOnTheGround;} set {_isOnTheGround = value;}}
    [HideInInspector]
    //Player States
    public bool isMoving, isJumping;
    [HideInInspector]
    public int movingDirection;
    #endregion

    void Start() {
        rgdBdy2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetInputs();
        CoyoteHandler();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void GetInputs() {
        movementInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && canJump) Jump();
    }

    #region Movement
    void MovePlayer() {
        transform.position = new Vector2(transform.position.x + movementInput * movementSpeed * Time.deltaTime, transform.position.y);
        isMoving = movementInput != 0;
        if(movementInput > 0) movingDirection = 1;
        else if (movementInput < 0) movingDirection = -1;
    }

    void Jump() {
        rgdBdy2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }
    #endregion

    #region Handlers
    void CoyoteHandler() {
        if(isOnTheGround && !canJump) {
            canJump = true;
            coyoteCount = 0;
            isJumping = false;
            return;
        }

        if(!isOnTheGround && coyoteCount < coyoteTimer) coyoteCount += Time.deltaTime;
        if(coyoteCount >= coyoteTimer && canJump) canJump = false;
    }
    #endregion    
}