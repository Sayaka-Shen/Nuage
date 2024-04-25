using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Collision")]
    private GameObject _npcTriggered;
    private bool _isColliding = false;
    
    public bool IsColliding
    {
        get { return _isColliding; }
    }

    public GameObject NpcTriggered
    {
        get { return _npcTriggered; }
    }

    [Header("Dialogue")]
    private DialogueManager _dialogueManager;
    
    
    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        if (_isColliding && Input.GetKeyDown(KeyCode.E))
        {
            _dialogueManager.StartDialogue();
        }
    }

    #region Functions Collision
    
    private void OnTriggerEnter2D(Collider2D collider)
    {   
        if (collider.tag == "NPC")
        {
            _isColliding = true;
            _npcTriggered = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _isColliding = false;
        _npcTriggered = null;
    }

    #endregion
    
    // checkpoint dans le labyrinthe
}
