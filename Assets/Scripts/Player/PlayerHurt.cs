using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private Player player;
    private PlayerGFX playerGFX;

    [SerializeField] private float knockForce;

    void Start() {
        player = FindObjectOfType<Player>();
        playerGFX = FindObjectOfType<PlayerGFX>();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Enemy")) {
            player.GetComponent<Player>().GetHurt(knockForce);
            playerGFX.GetHurt();
        }
    }
}
