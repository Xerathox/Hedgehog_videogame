using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{

    public GameObject explosion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player") 
        {
            Explode();
            PlayerHealthController.instance.DealDamage();
        }
    }

    public void Explode() 
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }


}
