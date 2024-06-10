using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody2D rgdBdy2d;

    [Header("Enemy Basics")]
    [SerializeField] private int life;
    [SerializeField] protected float movementSpeed;
    private float initialMovementSpeed;
    private float resetSpeedTime = 0.2f;
    protected enum EMoveType {
        ONLY_LEFT,
        ONLY_RIGHT,
        CHASE_PLAYER
    }
    [SerializeField] protected EMoveType moveType;
    private IMovementStrategy movementStrategy;

    [Header("Invicible Data")]
    private float invicibilityTime = 0.2f;
    private float invicibilityTimeCount;

    [Header("Hurt Data")]
    [SerializeField] private float knockForce;
    [SerializeField] private GameObject deathPrefab;


    void Start() {
        SetComponents();
    }

    void Update() {
        EnemyConstantsChecker();
    }

    void FixedUpdate() {
        MoveEnemy();
    }

    // Check some constants to enemy, like if it is invicible, or if collide in a death collider
    protected void EnemyConstantsChecker() {
        if(invicibilityTimeCount < invicibilityTime) invicibilityTimeCount += Time.deltaTime;
    }

    protected void SetComponents() {
        SetMovementStrategy();
        rgdBdy2d = GetComponent<Rigidbody2D>();
        initialMovementSpeed = movementSpeed;
    }

    public void GetHurt(int knockDirection) {
        if(invicibilityTimeCount < invicibilityTime) return;

        life--;
        invicibilityTimeCount = 0;
        movementSpeed = 0;

        rgdBdy2d.velocity = Vector3.zero;
        rgdBdy2d.AddForce(new Vector2(knockForce * knockDirection, 0f), ForceMode2D.Impulse);

        if(life <= 0) Die();
        else StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed() {
        yield return new WaitForSeconds(resetSpeedTime);
        movementSpeed = initialMovementSpeed;
    }

    void Die() {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
  
    /// <summary>
    /// The enemies by default move right to left
    /// </summary>
    virtual public void MoveEnemy() {
        movementStrategy.Move(transform, movementSpeed);
    }

    void SetMovementStrategy() {
        switch(moveType) {
            case EMoveType.ONLY_RIGHT: movementStrategy = FindObjectOfType<MoveOnlyRight>().GetComponent<MoveOnlyRight>(); break;
            case EMoveType.CHASE_PLAYER: movementStrategy = FindObjectOfType<MoveChasePlayer>().GetComponent<MoveChasePlayer>(); break;
            default: movementStrategy = FindObjectOfType<MoveOnlyLeft>().GetComponent<MoveOnlyLeft>(); break;
        }
    }
}
