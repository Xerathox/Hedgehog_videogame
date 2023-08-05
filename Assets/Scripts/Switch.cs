using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectSwitch;

    private SpriteRenderer theSR;
    public Sprite downSprite;

    private bool hasSwitched;

    public bool deactivateOnSwitch;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>(); //Spriterenderer de nuestro switch
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if(other.tag == "Player" && !hasSwitched) //si el jugador se cruza con el switch y este no está cambiado
       	{
			if(deactivateOnSwitch) 
        	{
	            objectSwitch.SetActive(false);
        	}
        	else
        	{
	            objectSwitch.SetActive(true);
        	}
			
		    theSR.sprite = downSprite;
	   		hasSwitched = true;
        }



    }


}
