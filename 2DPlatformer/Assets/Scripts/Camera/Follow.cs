using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objToFollow;
    public float lerpSpeed = 1.0f;
    public Vector3 offset = new Vector3(0, 0, 0);


    private Transform transformToFollow;
    // Start is called before the first frame update
    void Start()
    {
        transformToFollow = objToFollow.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transformToFollow.position + offset, lerpSpeed);
    }
}
