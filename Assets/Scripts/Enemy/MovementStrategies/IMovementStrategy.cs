using UnityEngine;

interface IMovementStrategy
{
    public void Move(Transform enemyTransform, float movementSpeed);
}
