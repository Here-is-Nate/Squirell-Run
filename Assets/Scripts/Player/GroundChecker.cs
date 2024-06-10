using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player player;

    [Header("Collider Controlls")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float colliderRadius;

    void Start() {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    void Update() {
        CheckGround();
    }

    void CheckGround() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, colliderRadius, groundLayer);

        if(hit) player.isOnTheGround = true;
        else player.isOnTheGround = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }
}
