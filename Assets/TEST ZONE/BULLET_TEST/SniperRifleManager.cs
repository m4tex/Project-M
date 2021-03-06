using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifleManager : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform shootPoint;
    public Animator gunAnim;
    public Animator shootAnim;
    float T = 0;
    float reloadTime = 1f;
    
    void Update()
    {
        T += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && T >= reloadTime)
        {
            T = 0;
            Shoot();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            gunAnim.SetBool("isZoomed", true);
        }
        else
        {
            gunAnim.speed = 1f;
            gunAnim.SetBool("isZoomed", false);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.3f, 0.1f);
            if (gunAnim.GetCurrentAnimatorStateInfo(0).IsName("ZoomedIddle") && Input.GetKey(KeyCode.Mouse1))
            {
                gunAnim.speed = Mathf.Lerp(gunAnim.speed, 0.01f, Time.deltaTime * 8f);
            }
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 1f);
            gunAnim.speed = 1f;
        }
    }


    void Shoot()
    {
        shootAnim.SetTrigger("Shoot");

        //Parabolic shoot code here
    }
}
