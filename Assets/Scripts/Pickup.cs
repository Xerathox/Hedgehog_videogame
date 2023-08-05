using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;
    private bool isCollected;
    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem) //Si recogimos una gema
            {
                LevelManager.instance.gemsCollected++; //Contamos las gemas recogidas para el nivel                
                UIController.instance.UpdateGemCount(); //Actualizamos las gemas en ese nivel
                Instantiate(pickupEffect, transform.position, transform.rotation); //Hacemos que la gema desaparezca con su función pickupEffect
                
                AudioManager.instance.PlaySFX(6); //Hacemos que la gema suene cuando desaparezca 
                isCollected = true; //Ponemos que esa gema ya se recogió
                Destroy(gameObject); //Destruimos esa gema
            }        

            if (isHeal) //Si recogimos un corazón
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth) //Si recogemos un corazon y no tenemos la vida al máximo
                {
                    PlayerHealthController.instance.HealPlayer(); //Nos curamos
                    AudioManager.instance.PlaySFX(7); //Hacemos el sonido de curación
                    isCollected = true; //Ponemos que ese corazón ya se recogió
                    Destroy(gameObject); //Destruimos ese corazón
                }
            }
        }
    }
}
