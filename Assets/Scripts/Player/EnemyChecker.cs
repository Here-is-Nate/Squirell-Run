using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    private Player player;

    void Start() {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Enemy")) {
            player.EnemyJump();
            collider.gameObject.GetComponent<Enemy>().GetHurt(player.movingDirection);
        }
    }
}
