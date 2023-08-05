using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{
    public BossTankController bossCont; //controlador del jefe

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y) //tenemos que ser el jugador y tenemos que estar por encima del jefe
        {
            bossCont.TakeHit(); //el jefe recibe daño

            PlayerController.instance.Bounce(); //el jugador rebota

            gameObject.SetActive(false); //le damos un breve tiempo de invulnerabilidad al jefe
        }
        
        
    }

}
