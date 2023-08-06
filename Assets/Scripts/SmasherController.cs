using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherController : MonoBehaviour
{
    public float moveSpeed;
    public Transform topPoint, bottomPoint; // Nuevo punto superior e inferior

    private bool movingUp; // Nuevo booleano para controlar el movimiento hacia arriba y hacia abajo

    private Rigidbody2D theRB;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        topPoint.parent = null;
        bottomPoint.parent = null;

        movingUp = true; // Empezamos moviéndonos hacia arriba
        moveCount = moveTime;
    }

    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingUp)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed); // Movemos hacia arriba

                if (transform.position.y > topPoint.position.y) // Si alcanza el punto superior
                {
                    movingUp = false; // Cambiamos la dirección hacia abajo
                }
            }
            else
            {
                theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed); // Movemos hacia abajo

                if (transform.position.y < bottomPoint.position.y) // Si alcanza el punto inferior
                {
                    movingUp = true; // Cambiamos la dirección hacia arriba
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = Vector2.zero; // Detenemos el movimiento vertical

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 1.25f);
            }
        }
    }
}
