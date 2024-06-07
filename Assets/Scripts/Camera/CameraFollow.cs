using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float lockedYPosition;

    // Start is called before the first frame update
    void Start()
    {
        lockedYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, lockedYPosition);
    }
}
