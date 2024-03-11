using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : Entity
{
    float maxRadius = 5f;
    float minRadius = 2f;
    float time;
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
        time = Time.realtimeSinceStartup;
        currentHealth = 1000f;
        //CanTP = true;
        StartCoroutine(Wait());
    }
    public void Update()
    {
    }

    private void FixedUpdate() {
        Vector2 lookDir = player.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }

    private void Stage1(){
        if(time == 3)
            {CanPen = true;}

        if(time == 6)
            {CanTP = true;}
        
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
        pen.transform.Rotate(0f, 0f, 1f, Space.Self);
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
        float seconds;
        while(true){
            if(CanTP){
                Teleport();
                seconds = 3f;
                yield return new WaitForSeconds(seconds);
            }
            else if(CanPen){
                PenThrowing();
                seconds = 1f;
                yield return new WaitForSeconds(seconds);
                
            }


            if(currentHealth <= 0){
                //Death Animation
            }
            
            else if(currentHealth <= 250){
                Stage4();
            }

            else if (currentHealth <= 500){
                Stage3();
            }

            else if(currentHealth <= 750){
                Stage2();
            }

            else{
                Stage1();
            }
        }
    }

    /*
    IEnumerator Attacks(){
        while(true){
            Attack1();
            yield return new WaitForSeconds(5f);

            Attack2();
            yield return new WaitForSeconds(5f);

            if(hp < 50){
                LowHpAttack();
                yield return new WaitForSeconds(10f);
            }
            else if(ammo == 0){
                reload();
                yield return new WaitForSeconds(7f);
            }
        }

    }
    */
}
