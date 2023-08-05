using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameObject deathEffect;

    [Range(0,100)] public float chanceToDrop; //un numero aleatorio entre 0 y 100

    public GameObject collectible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") //si colisionamos contra un enemigo
        {
            other.transform.parent.gameObject.SetActive(false); //este se desactiva
            Instantiate(deathEffect, other.transform.position, other.transform.rotation); //activamos el efecto de muerte
            PlayerController.instance.Bounce(); //el jugador rebota
            float dropSelect = Random.Range(0,100f); //la variable dropSelect guardará el valor random del 0 al 100

            if(dropSelect <= chanceToDrop) //si el dropSelect es menor o igual a chanceToDrop
            {
                Instantiate(collectible,other.transform.position, other.transform.rotation); //se genera un coleccionable
            }
            
            AudioManager.instance.PlaySFX(3); 

        }        
    }
}
