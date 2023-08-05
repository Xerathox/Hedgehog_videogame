using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;
        moveCount = moveTime;
    }

    void Update()
    {
        if(moveCount > 0) //Si el contador de movimiento es mayor que 0
        {
            moveCount -= Time.deltaTime; //Disminuimos su tiempo

            if (movingRight) //Si se está moviendo a la derecha
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y); //Lo movemos a la derecha 

                theSR.flipX = true; //Volteamos el sprite horizontalmente

                if (transform.position.x > rightPoint.position.x) //Si la posición x traspasó la posición de la derecha hasta donde debería llegar
                {
                    movingRight = false; //Desactivamos el booleano que le permite al enemigo moverse a la derecha
                }
            } 
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y); //Empezamos a movernos hacia la izquierda

                theSR.flipX = false; //recuperamos el sprite a su posición original

                if (transform.position.x < leftPoint.position.x) //si la posición x traspasó la posición de la izquierda hasta donde debería llegar
                {
                    movingRight = true; //Activamos el booleano que le permite al enemigo moverse a la derecha
                }
            }

            if(moveCount <= 0) //si el contador del movimiento es 0 o menor que 0
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f); //esperamos un tiempo entre .75s y 1.25s para poder empezar a movernos nuevamente
            }
            anim.SetBool("isMoving",true); //activamos la animación del personaje para que se empiece a mover de nuevo
        }
        else if (waitCount > 0) //si el contador de espera es mayor que 0
        {
            waitCount -= Time.deltaTime; //disminuye el contador de espera
            theRB.velocity = new Vector2(0f, theRB.velocity.y); //el enemigo deja de moverse

            if(waitCount <= 0) //si el contador de espera es 0 o menor
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 1.25f); //le asignamos un tiempo de .75s a 1.25s a moveCount para empezar a movernos nuevamente
            }
            anim.SetBool("isMoving",false); //paramos la animación de movernos
        }
                        
    }    
}
