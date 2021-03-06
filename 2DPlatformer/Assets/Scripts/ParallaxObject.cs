using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{

    public float xParallaxScale = 0.5f;
    public float yParallaxScale = 0.5f;
    
    public float xParallaxScaleRange = 0.1f;
    public float yParallaxScaleRange = 0.1f;

    public float objectXSpeed = 1f;
    public float objectXSpeedRange = 0.1f;

    private Camera mainCamera;
    private Transform myTransform;
    private float xOffset = 0;
    private float startXPos = 0;
    private float startYPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        //add random range to value
        xParallaxScale += Random.Range(-xParallaxScaleRange, xParallaxScaleRange);
        yParallaxScale += Random.Range(-yParallaxScaleRange, yParallaxScaleRange);
        objectXSpeed += Random.Range(-yParallaxScaleRange, yParallaxScaleRange);

        //clamp 0 to 1
        xParallaxScale = Mathf.Clamp(xParallaxScale, 0, 1);
        yParallaxScale = Mathf.Clamp(yParallaxScale, 0, 1);
        objectXSpeed = Mathf.Clamp(objectXSpeed, 0, 1);

        mainCamera = Camera.main;
        myTransform = GetComponent<Transform>();
        startXPos = myTransform.position.x - (mainCamera.transform.position.x * xParallaxScale);
        startYPos = myTransform.position.y - (mainCamera.transform.position.y * yParallaxScale);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Transform cameraTransform = mainCamera.transform;

        //Offset is x offset of cloud
        xOffset += objectXSpeed * Time.deltaTime;

        transform.position = new Vector3(startXPos + xOffset + (cameraTransform.position.x * xParallaxScale), startYPos + (cameraTransform.position.y * yParallaxScale), myTransform.position.z) ;
    }
}
