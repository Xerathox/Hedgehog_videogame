using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; //inicializamos la variable como una estancia para poder llamarlo desde el script de DamagePlayer
    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;
    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth; //setapeamos la vida
        theSR = GetComponent<SpriteRenderer>(); //inicializamos el renderizador
    }

    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; //si soy invencible, el counter de invencibilidad disminuye

            if(invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r,theSR.color.g,theSR.color.b,1f); //soy un poco transparente mientras soy invencible
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--; //si me hacen daño, mi vida baja
            PlayerController.instance.anim.SetTrigger("Hurt"); //cambia el sprite a sprite de daño
            AudioManager.instance.PlaySFX(9); //suena sonido de golpe a Player

            if (currentHealth <= 0) //si te dañan y mueres
            {
                currentHealth = 0; //su vida será 0
                Instantiate(deathEffect, PlayerHealthController.instance.transform.position, PlayerHealthController.instance.transform.rotation); //traemos la animación de muerte del Player
                AudioManager.instance.PlaySFX(8); //suena el sonido de muerte
                LevelManager.instance.RespawnPlayer(); //respawneo
            }
            else // si te dañan, pero no mueres
            {
                invincibleCounter = invincibleLength; //el valor de la duración de invencibilidad se iguala con el contador de invencibilidad
                theSR.color = new Color(theSR.color.r,theSR.color.g,theSR.color.b,.5f); //me vuelvo un poco transparente
                PlayerController.instance.Knockback(); //se le aplica el knockback al personaje
            }

        UIController.instance.UpdateHealthDisplay(); //actualiza los íconos de los corazones en pantalla
        }
    }

    public void HealPlayer() //cuando te curas con algún objeto
    {
        currentHealth += 2; //se aumenta tu vida en 2
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth; //tu vida no puede aumentar más de lo máximo
        }
        UIController.instance.UpdateHealthDisplay(); //actualiza los íconos de los corazones en pantalla
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform; //cuando entras a una plataforma, el Player se convierte en hijo de la plataforma para que esta pueda moverlo a el cuando esta se mueve
        }        
    }

    
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null; //cuando sales de una plataforma, el Player deja de ser hijo de la plataforma
        }          
    }



}
