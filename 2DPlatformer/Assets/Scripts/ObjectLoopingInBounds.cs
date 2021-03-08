using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoopingInBounds : MonoBehaviour
{
    public float xSpeed = 1.0f;
    public float worldMinX = -100;
    public float worldMaxX = 100;

    private void Start()
    {
       
    }

    //Draws min to max in world
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(worldMinX, -1000, 0), new Vector3(worldMinX, 1000, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(worldMaxX, -1000, 0), new Vector3(worldMaxX, 1000, 0));

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * xSpeed * Time.deltaTime);

        float newX = Mathf.Clamp(transform.position.x, worldMinX, worldMaxX);

        if(newX != transform.position.x)
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
