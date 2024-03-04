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
    bool CanPen = false;
    private float targetTime;
    public Rigidbody2D rb;
    public GameObject penPrefab;
    public Transform firePoint;
    public Rigidbody2D player;
    public float throwForce = 5f;

    public void Start()
    {
        currentHealth = 1000f;
        //CanTP = true;
        StartCoroutine(Wait());
    }
    public void Update()
    {
    }

    private void FixedUpdate() {
        Vector2 lookDir = player.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }

    private void Stage1(){
        CanTP = false;
        CanPen = true;
    }

    private void Stage2(){

    }

    private void Stage3(){

    }

    private void Stage4(){
        
    }

    private void PenThrowing(){
        GameObject pen = Instantiate(penPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D PenRb = pen.GetComponent<Rigidbody2D>();
        PenRb.AddForce(firePoint.right * throwForce, ForceMode2D.Impulse);
        CanPen = false;
    }


    private void Teleport(){
        Vector2 randomPos = Random.insideUnitCircle * (maxRadius - minRadius);
        transform.position = randomPos.normalized * minRadius + randomPos;
        CanTP = false;
    }

    IEnumerator Wait()
    {
        float seconds = 2f;
        while(true){
            yield return new WaitForSeconds(seconds);
            if(CanTP){
                Teleport();
                seconds = 3f;
            }
            else if(CanPen){
                PenThrowing();
                seconds = 1f;
            }

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
    }

}
