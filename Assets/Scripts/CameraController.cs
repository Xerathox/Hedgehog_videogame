using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    public Transform farBackground, middleBackground;
    public float minHeight, maxHeight;
    private Vector2 lastPos;
    public bool stopFollow;
    
    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z); //seguimiento de la cámara al jugador

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y); //cuanta es la cantidad de movimiento que tenemos que asignarle a nuestros backgrounds

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f); //seguimiento del farBackground a la misma velocidad del jugador
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f; //seguimiento del middleBackground a una velocidad más lenta que la del jugador

            lastPos = transform.position; //guardar la última posición 
        }
    }
}
