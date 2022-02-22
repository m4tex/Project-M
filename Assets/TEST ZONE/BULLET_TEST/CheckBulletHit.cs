using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will check if the bullet hit anything since last update
public class CheckBulletHit : MonoBehaviour
{
    private Vector3 lastPos;
    public GameObject hitmarkPrefab;

    private void Awake()
    {
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        CheckHit();

        lastPos = transform.position;
    }



    //Did we hit a target
    void CheckHit()
    { 
        Vector3 currentPos = transform.position;

        Vector3 fireDirection = (currentPos - lastPos).normalized;
        
        float fireDistance = (currentPos - lastPos).magnitude;

        // Vector3 prediction = currentPos + (currentPos - lastPos).normalized * Time.fixedDeltaTime;
        Debug.DrawRay(currentPos, fireDirection, Color.green, 10f);
        if (!Physics.Raycast(currentPos, fireDirection, out RaycastHit hit, fireDistance)) return;
        if (!hit.collider.CompareTag("Target")) return;

        GameObject hitmark = Instantiate(hitmarkPrefab);
        hitmark.transform.position = hit.point;
        
        Debug.Log("Hit target!");

        //Move the bullet to where we hit
        transform.position = hit.point;

        //Deactivate the script that moves the bullet, so we can see where it hit
        GetComponent<MoveBullet>().enabled = false;
    }
}
