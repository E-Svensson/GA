using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : Entity
{
    float maxRadius = 5f;
    float minRadius = 2f;
    bool CanTP = false;
    private void Start()
    {
        currentHealth = 1000f;
    }
    private void Update()
    {
        if(CanTP)
            StartCoroutine(Teleport());


        if(currentHealth <= 750){
            Stage2();
        }
        else if (currentHealth <= 500)
        {
            Stage3();
        }
        else if(currentHealth <= 250)
        {
            Stage4();
        }
        else if(currentHealth <= 0)
        {
            //Death Animation
        }
        else{
            Stage1();
        }
    }

    private void Stage1(){

    }

    private void Stage2(){

    }

    private void Stage3(){

    }

    private void Stage4(){
        
    }

    private void PenThrowing(){
        
    }


    IEnumerator Teleport(){
        Vector2 randomPos = Random.insideUnitCircle * (maxRadius - minRadius);
        transform.position = randomPos.normalized * minRadius + randomPos;
        CanTP = false;
        yield return new WaitForSeconds(1f);
        CanTP = true;
    }

}
