using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Target yang diikuti kamera (misal: Player)
    public Vector3 offset;   // Jarak offset dari target agar framing pas

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // Ikuti target dengan offset setiap frame
    }
}
