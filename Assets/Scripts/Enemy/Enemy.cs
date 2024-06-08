using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rgdBdy2d;

    [Header("Enemy Basics")]
    [SerializeField] private int life;
    [SerializeField] private float movementSpeed;
    private float initialMovementSpeed;
    private float resetSpeedTime = 0.2f;

    [Header("Invicible Data")]
    private float invicibilityTime = 0.2f;
    private float invicibilityTimeCount;

    [Header("Hurt Data")]
    [SerializeField] private float knockForce;
    [SerializeField] private GameObject deathPrefab;


    void Start() {
        rgdBdy2d = GetComponent<Rigidbody2D>();
        initialMovementSpeed = movementSpeed;
    }

    void Update() {
        if(invicibilityTimeCount < invicibilityTime) invicibilityTimeCount += Time.deltaTime;
    }

    void FixedUpdate() {
        MoveEnemy();
    }

    public void GetHurt(int knockDirection) {
        if(invicibilityTimeCount < invicibilityTime) return;

        life--;
        invicibilityTimeCount = 0;
        movementSpeed = 0;

        rgdBdy2d.velocity = Vector3.zero;
        rgdBdy2d.AddForce(new Vector2(knockForce * knockDirection, 0f), ForceMode2D.Impulse);

        if(life <= 0) Die();

        StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed() {
        yield return new WaitForSeconds(resetSpeedTime);
        movementSpeed = initialMovementSpeed;
    }

    void Die() {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    virtual public void MoveEnemy() {
        transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime * (-1f), transform.position.y);
    }
    
}
