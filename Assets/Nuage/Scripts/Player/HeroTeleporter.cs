using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeleporter : MonoBehaviour
{
    [Header("Teleporter")] 
    [SerializeField] private GameObject _teleporterGymnase;
    [SerializeField] private GameObject _teleporterLabyrinth;

    private void _TeleportLabyrinth()
    {
        transform.position = _teleporterLabyrinth.transform.position;
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Gymnase")
        {
            transform.position = _teleporterGymnase.transform.position;
        }
    }
}
