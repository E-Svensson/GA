using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : MonoBehaviour
{
    float maxRadius = 5f;
    float minRadius = 2f;
    bool CanTP = true;
    private void Start()
    {

    }
    private void Update()
    {
        if(CanTP)
            StartCoroutine(Teleport());
    }

    private void Stage1(){

    }

    private void Stage2(){

    }

    private void Stage3(){

    }

    private void Stage4(){
        
    }


    IEnumerator Teleport(){
        Vector2 randomPos = Random.insideUnitCircle * (maxRadius - minRadius);
        transform.position = randomPos.normalized * minRadius + randomPos;
        CanTP = false;
        yield return new WaitForSeconds(1f);
        CanTP = true;
    }

}
