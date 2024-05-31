using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerAimController : MonoBehaviour
{
    private Transform aimTransform;
    public GameObject gun;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    private float fireRate = 0.2f;
    public bool CanFire = true;
    public bool Reloading = false;
    public float Ammo = 0;
    private float MaxAmmo = 12f;
    public TextMeshProUGUI tmp;
    private void Awake(){
        aimTransform = transform.Find("Aim");
    }

    private void Start(){
        Ammo = MaxAmmo;
    }

    private void Update(){
        Aiming();
        Shooting();

        if(Ammo <= 0)
        {
            CanFire = false;
            StartCoroutine(Realod());
        }

        if(Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Realod());
        

        if(Reloading)
            tmp.text = "Reloading...";
        else
            tmp.text = $"{Ammo}/{MaxAmmo}";
        
    }   

    private void Shooting(){
        if(Input.GetMouseButtonDown(0) && CanFire || Ammo == 0){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Ammo--;
            CanFire = false;
            StartCoroutine(FireRate());
        }
    }

    private void Aiming(){
        Vector3 mousePosition = GetMouseWorldPosition();
 
        Vector3 aimDirection = (mousePosition - transform.position).normalized;   
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;  
        aimTransform.eulerAngles = new Vector3(0,0, angle);

        if(angle < 89 && angle > -89 ){
            gun.transform.localScale = new Vector3(1,1,1);
        }
        else
            gun.transform.localScale = new Vector3(1, -1, 1);
        

        if(PlayerMovement.walkDirection == WalkDirection.Up){
            gun.transform.localPosition = new Vector3(0.3226f, -0.0217f, 1f);
        }
        else{
            gun.transform.localPosition = new Vector3(0.3226f, -0.0217f, -1f);
        }
    }
    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    } 

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireRate);
        CanFire = true;
    }

    IEnumerator Realod()
    {
        Reloading = true;
        CanFire = false;
        yield return new WaitForSeconds(2f);
        Reloading = false;
        Ammo = MaxAmmo;
        CanFire = true;
    }
}