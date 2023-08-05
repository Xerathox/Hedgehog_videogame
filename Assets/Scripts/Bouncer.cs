using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private Animator anim;

    public float bounceForce = 10;


    void Start()
    {
       anim = GetComponent<Animator>(); 
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x,bounceForce);
            AudioManager.instance.PlaySFX(13); 

            anim.SetTrigger("Bounce");
            
        }
        
    }

}
