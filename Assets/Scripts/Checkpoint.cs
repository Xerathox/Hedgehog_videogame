using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite cpOn, cpOff;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) //si nos chocamos con el jugador
        {
            CheckpointController.instance.DeactivateCheckpoints(); //desactivamos todos los checkpoints
            theSR.sprite = cpOn; //prendemos el checkpoint en el que estamos 
            CheckpointController.instance.SetSpawnPoint(transform.position); //seteamos nuestra posición allí por si morimos

        }        
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff; //apagamos el sprite
    }

}
