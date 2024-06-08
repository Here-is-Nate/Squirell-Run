using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float destroyTime = 0.5f;
    private float destroyTimeCount;

    void Update() {
        destroyTimeCount += Time.deltaTime;

        if(destroyTimeCount >= destroyTime) Destroy(gameObject);
    }
}
