using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{

    public Vector2 minPos, maxPos;

    public Transform target;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x); //seguimiento de la cámara con un poquito de retraso para el eje x
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y); //seguimiento de la cámara con un poquito de retraso para el eje y

        transform.position = new Vector3(xPos, yPos,transform.position.z);

    }
}
