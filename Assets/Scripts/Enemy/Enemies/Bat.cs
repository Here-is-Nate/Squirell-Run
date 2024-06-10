using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [Header("Bat Data")]
    [SerializeField] private bool startHanging;
    [SerializeField] private bool chasePlayer;

    [Header("See Player Data")]
    [SerializeField] private float lookPlayerRadius;
    [SerializeField] private LayerMask playerLayer;

    void Start() {
        SetComponents();

        if(startHanging) rgdBdy2d.gravityScale = 0;
    }

    void Update() {
        EnemyConstantsChecker();

        Collider2D hit = Physics2D.OverlapCircle(transform.position, lookPlayerRadius, playerLayer);

        if(!startHanging) SeePlayer();

        if(hit && chasePlayer) SeePlayer();
    }

    void SeePlayer() {
        movementSpeed = 2f;
        rgdBdy2d.gravityScale = 0.1f;
        GetComponent<Animator>().SetBool("Hanging", false);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, lookPlayerRadius);
    }

}
