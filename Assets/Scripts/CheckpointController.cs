using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    public Checkpoint[] checkpoints;
    public Vector3 spawnPoint;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>(); //inicializamos los checkpoints de la escena
        spawnPoint = PlayerController.instance.transform.position; //guardamos la posición del jugador en el checkpoint
    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++) 
        {
            checkpoints[i].ResetCheckpoint(); //reseteamos todos los checkpoints
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint; //el nuevo spawnpoint que tocamos ahora es el spawnpoint por default
    }

    
}
