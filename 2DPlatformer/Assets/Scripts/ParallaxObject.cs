using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{

    public float parallaxDivisor = 0.5f;
    public float objectXSpeed = 1f;

    private Camera mainCamera;
    private Transform myTransform;
    private float xOffset = 0;
    private float startXPos = 0;
    private float startYPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myTransform = GetComponent<Transform>();
        startXPos = myTransform.position.x - (mainCamera.transform.position.x * parallaxDivisor);
        startYPos = myTransform.position.y - mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = mainCamera.transform;

        //Offset is x offset of cloud
        xOffset += objectXSpeed * Time.deltaTime;
        //transform.position = new Vector3(startXPos + offset + (cameraTransform.position.x * parallaxDivisor), myTransform.position.y, myTransform.position.z) ;

        Vector3 newPosition = new Vector3(startXPos + xOffset + (cameraTransform.position.x * parallaxDivisor), startYPos + cameraTransform.position.y, myTransform.position.z) ;
        transform.position = newPosition; // Vector3.Lerp(transform.position, newPosition, 0.1f);
    }
}
