using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    public Vector3 offset;

    void Start()
    {
        //offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToFollow != null)
        {
            transform.position = new Vector3(objectToFollow.transform.position.x, 0, objectToFollow.transform.position.z) + offset;
        }
    }
}
