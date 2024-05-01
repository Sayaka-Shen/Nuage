using System;
using Unity.VisualScripting;
using UnityEngine;

public class HeroTeleporter : MonoBehaviour
{
    [Header("Teleporter")] 
    [SerializeField] private GameObject _teleporterGymnase;
    [SerializeField] private GameObject _teleporterLabyrinth;
    [SerializeField] private GameObject _teleporterDream;
    
    [Header("Physics")] 
    private Rigidbody2D _rigidbody;

    [Header("Hero")] 
    [SerializeField] private HeroEntity _heroEntity;
    [SerializeField] private HeroEntityTopDown _heroEntityTopDown;
    
    [SerializeField] private HeroController _heroController;
    [SerializeField] private HeroControllerTopDown _heroControllerTopDown;
    
    
    // Function is public to access it easily 
    public void TeleportLabyrinth()
    {
        transform.position = _teleporterLabyrinth.transform.position;

        _heroController.enabled = false;
        _heroControllerTopDown.enabled = true;

        _heroEntity.enabled = false;
        _heroEntityTopDown.enabled = true;
    }

    public void TeleportDream()
    {
        transform.position = _teleporterDream.transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Gymnase")
        {
            transform.position = _teleporterGymnase.transform.position;
        }
    }
}
