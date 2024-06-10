using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChasePlayer : MonoBehaviour, IMovementStrategy
{   
    public void Move(Transform enemyTransform, float movementSpeed)
    {
        Transform playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();

        if(enemyTransform.position.x - playerTransform.position.x >= 0) 
            enemyTransform.GetComponent<SpriteRenderer>().flipX = false;
        else 
            enemyTransform.GetComponent<SpriteRenderer>().flipX = true;
        
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerTransform.position, movementSpeed * Time.deltaTime);
    }
}
