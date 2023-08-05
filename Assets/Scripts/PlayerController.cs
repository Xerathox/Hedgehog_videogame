using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    [Header("Movimiento")]
    public float moveSpeed;

    [Header("Salto")]
    private bool canDoubleJump;
    public float jumpForce;
    public float bounceForce;

    [Header("Componentes")]
    public Rigidbody2D theRB; 

    [Header("Animator")]
    public Animator anim;
    private SpriteRenderer theSR;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;

    public float knockbackLength, knockbackForce;
    private float knockbackCounter;

    public bool stopInput;

    private void Awake()
    {
        instance = this;        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        if(!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockbackCounter <= 0) 
            {
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y); //accedemos a la variable "Velocity" y usamos el "Input Manager Horizontal" para poder mover al personaje horizontalmente
                isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround); //con esto el Player puede detectar el piso

                if (isGrounded) 
                {
                    canDoubleJump = true; //si estás en el suelo, puedes saltar
                }

                if (Input.GetButtonDown("Jump")) //si presionamos saltar
                {
                    if (isGrounded) //si estamos en el suelo
                    { 
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); //saltamos
                        AudioManager.instance.PlaySFX(10); // sonido de saltar               
                    }
                    else if (canDoubleJump) //si podemos hacer doble salto
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); //saltamos
                        AudioManager.instance.PlaySFX(10); // sonido de saltar  
                        canDoubleJump = false; //ya no podemos hacer doble salto
                    }
                }

                if (theRB.velocity.x < 0) //si me estoy moviendo hacia la izquierda
                {
                    theSR.flipX = true; //voltear el sprite
                }
                else if (theRB.velocity.x > 0) //si me estoy moviendo hacia la derecha
                {
                    theSR.flipX = false; //volver el sprite a su forma original
                }
            }
            else 
            {
                knockbackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockbackForce,theRB.velocity.y); //si me hago daño y estoy mirando hacia la derecha, me empuja hacia la izquierda y arriba 
                }
                else 
                {
                    theRB.velocity = new Vector2(knockbackForce,theRB.velocity.y); //si me hago daño y estoy mirando hacia la izquierda, me empuja hacia la derecha y arriba
                }            
            }
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Knockback() 
    {
        knockbackCounter = knockbackLength;
        theRB.velocity = new Vector2(0f,knockbackForce);
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }
}