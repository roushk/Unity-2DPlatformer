using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objToFollow;

    private Transform transformToFollow;
    // Start is called before the first frame update
    void Start()
    {
        transformToFollow = objToFollow.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transformToFollow.position;
    }
}
