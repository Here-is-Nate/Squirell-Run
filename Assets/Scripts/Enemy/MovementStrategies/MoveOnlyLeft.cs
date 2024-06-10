using UnityEngine;

public class MoveOnlyLeft : MonoBehaviour, IMovementStrategy
{   
    public void Move(Transform enemyTransform, float movementSpeed)
    {
        enemyTransform.position = new Vector2(enemyTransform.position.x - movementSpeed * Time.deltaTime, enemyTransform.position.y);
    }
}
