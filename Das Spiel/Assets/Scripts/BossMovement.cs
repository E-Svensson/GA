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
    private float targetTime;
    bool Repeat = true;
    private void Start()
    {
        currentHealth = 1000f;
        //CanTP = true;
    }
    private void Update()
    {
        targetTime -= Time.deltaTime;
        if(CanTP && Repeat){Teleport();}

        if(currentHealth <= 750){
            Stage2();
        }

        else if (currentHealth <= 500){
            Stage3();
        }

        else if(currentHealth <= 250){
            Stage4();
        }
        else if(currentHealth <= 0){
            //Death Animation
        }
        else{
            Stage1();
        }
    }

    private void Stage1(){
        targetTime = 3f;
        if(targetTime <= 0)
        {
            CanTP = true;
        }
    }

    private void Stage2(){

    }

    private void Stage3(){

    }

    private void Stage4(){
        
    }

    private void PenThrowing(){
        
    }


    private void Teleport(){
        Vector2 randomPos = Random.insideUnitCircle * (maxRadius - minRadius);
        transform.position = randomPos.normalized * minRadius + randomPos;
        Repeat = false;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
