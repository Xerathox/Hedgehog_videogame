using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;
    public float moveSpeed = 10f;
    public bool levelLoading;

    public LSManager theManager;
    
    void Start()
    {
        
    }

    
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed* Time.deltaTime); //velocidad a la que se moverá el jugador

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)
        {
            if(Input.GetAxisRaw("Horizontal") > .5f) //movimiento hacia la derecha
            {
                if(currentPoint.right != null) {
                    SetNextPoint(currentPoint.right);                
                }
            }
            if(Input.GetAxisRaw("Horizontal") < -.5f) //movimiento hacia la izquierda
            {
                if(currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);                
                }
            }
            if(Input.GetAxisRaw("Vertical") > .5f) //movimiento hacia arriba
            {
                if(currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);                
                }
            }
            if(Input.GetAxisRaw("Vertical") < -.5f) //movimiento hacia abajo
            {
                if(currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);                
                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked) //si el nivel en el que estoy ee un nivel y el nivel a cargar tiene nombre, además de que no está bloqueado
            {
                LSUIManager.instance.ShowInfo(currentPoint); //muestro su información
                if(Input.GetButtonDown("Jump")) //y si presiono la tecla espacio
                {
                    levelLoading = true; //activamos el booleano para poder cargar el nivel
                    theManager.LoadLevel(); //cargamos el nivel
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint; //nos movemos al siguiente punto
        LSUIManager.instance.HideInfo(); //escondemos la información 
    }
}
