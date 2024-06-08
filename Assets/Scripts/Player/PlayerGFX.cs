using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    private Player player;
    private Animator playerAnimator;

    private int animationIndex;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationIndex();
        FlipSpriteX();

        playerAnimator.SetBool("OnTheGround", player.isOnTheGround);
    }

    void SetAnimationIndex() {
        animationIndex = player switch {
            {isDashing: true} => 3,
            {isJumping: true} => 2,
            {isMoving: true} => 1,
            _ => 0
        };

        playerAnimator.SetInteger("Transition", animationIndex);
    }

    void FlipSpriteX() {
        if(player.movingDirection == 1) GetComponent<SpriteRenderer>().flipX = false;
        else if(player.movingDirection == -1) GetComponent<SpriteRenderer>().flipX = true;
    }

    public void GetHurt() {
        playerAnimator.SetTrigger("Hurt");
    }
}
