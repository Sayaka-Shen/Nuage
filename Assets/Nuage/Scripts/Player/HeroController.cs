using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Entity")] 
    [SerializeField] private HeroEntity _entity;
    
    private void Update()
    {
        _entity.SetMoveX(_GetInputMoveX());
    }

    private float _GetInputMoveX()
    {
        float inputMoveX = 0f;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            inputMoveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveX = 1f;
        }

        return inputMoveX;
    }
}
