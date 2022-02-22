using System;
using System.Collections;
using System.Collections.Generic;
using TEST_ZONE;
using UnityEngine;
using UnityEngine.UI;

public class SniperController : MonoBehaviour
{
    //The bullet we will fire
    public GameObject sniperBullet;
    //The parent to the bullets to get a clean workspace
    public Transform bulletParentTrans;
    //The sniper scope image
    public Image sniperScopeImage;

    //Camera stuff
    private Camera mainCamera;
    //Need the initial camera FOV so we can zoom
    private float initialFOV;
    //Different zoom levels we can have zoom
    private int currentZoom = 1;

    //Used so we can only fire one bullet when pressing a key
    private bool canFire = true;



    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        initialFOV = mainCamera.fieldOfView;

        sniperScopeImage.enabled = false;
    }

    

    private void Update()
    {
        ZoomSight();

        FireBullet();
    }



    //The sniper rifle can zoom between 3 and 12 times
    private void ZoomSight()
    {
        //Zoom with mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentZoom += 1;

            ChangeCameraZoomSettings();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentZoom -= 1;

            ChangeCameraZoomSettings();
        }
    }



    //Change whatever needs to be changed when we zoom in/out
    private void ChangeCameraZoomSettings()
    {
        //Clamp zoom
        //Keep it between 1 and 11, then add 1 when zoom because zoom is between 3 and 12 times
        currentZoom = Mathf.Clamp(currentZoom, 1, 11);


        //Remove the scope sight if we are not zooming
        if (currentZoom == 1)
        {
            sniperScopeImage.enabled = false;

            //Change sensitivity

            //If the zoom is 6x, then the FOV is FOV / 6 (according to unscientific research on Internet)
            mainCamera.fieldOfView = initialFOV / (float)currentZoom;
        }
        else
        {
            sniperScopeImage.enabled = true;

            //Change sensitivity

            mainCamera.fieldOfView = initialFOV / ((float)currentZoom + 1f);
        }
    }
    
    //Fire a single bullet
    private void FireBullet()
    {
        //Sometimes when we move the mouse is really high in the webplayer, so the mouse cursor ends up outside
        //of the webplayer so we cant fire, despite locking the cursor, so add alternative fire button
        if ((Input.GetKeyDown(KeyCode.F)) && canFire)
        {
            
            //Create a new bullet
            GameObject newBullet = Instantiate(sniperBullet);

            //Parent it to get a clean workspace
            newBullet.transform.parent = bulletParentTrans;

            //Give it speed and position
            newBullet.GetComponent<ParabolicBullet>().Initialize(mainCamera.transform);
            
            canFire = false;
        }

        //Has to release the trigger to fire again
        if (Input.GetKeyUp(KeyCode.F))
        {
            canFire = true;
        }
    }
}