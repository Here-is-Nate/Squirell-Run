using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    void Start() {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Enemy")) {
            collider.gameObject.GetComponent<Enemy>().GetHurt(player.movingDirection);
        }
    }
}
