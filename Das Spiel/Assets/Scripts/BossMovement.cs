using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : MonoBehaviour
{
    int rndSec;
    int rndPos;
    private float nextAction;
    float maxRadius = 4f;
    float minRadius = 1f;
    void Start()
    {
        nextAction = Time.time + 2f;
        rndSec = Random.Range(2, 4);
        rndPos = Random.Range(2, 4);
        
    }
    void Update()
    {
        if(nextAction >= Time.time)
        {
            Vector2 randomPos = Random.insideUnitCircle * (maxRadius - minRadius);
            transform.position = randomPos.normalized * minRadius + randomPos;
            nextAction += 2f;
        }
        
    }

}
