using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnlyRight : MonoBehaviour, IMovementStrategy
{   
    public void Move(Transform enemyTransform, float movementSpeed)
    {
        enemyTransform.position = new Vector2(enemyTransform.position.x + movementSpeed * Time.deltaTime, enemyTransform.position.y);
        enemyTransform.GetComponent<SpriteRenderer>().flipX = true;        
    }
    
}
