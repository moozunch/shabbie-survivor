using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; //objxect to follow
    public Vector3 offset; //offset from the target object

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
