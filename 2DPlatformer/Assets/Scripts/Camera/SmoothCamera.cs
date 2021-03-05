using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{

    public Transform target;
    public Prime31.CharacterController2D targetRigid;

    public float smoothingSpeed = 5f;
    public float MovementVectorSmoothing = 0.4f;
    public Vector3 offset = new Vector3(0, 0.5f, -1);

    // Start is called before the first frame update
    void Start()
    {
        //camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 betterOffset = offset + new Vector3(targetRigid.velocity.x * MovementVectorSmoothing, targetRigid.velocity.y * MovementVectorSmoothing * 0.8f, -1);

        transform.position = Vector3.Lerp(transform.position, target.position + betterOffset, smoothingSpeed * Time.deltaTime);
    }
}
