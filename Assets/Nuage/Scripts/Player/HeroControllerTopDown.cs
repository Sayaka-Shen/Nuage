using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControllerTopDown : MonoBehaviour
{
    [Header("Entity")] 
    [SerializeField] private HeroEntityTopDown _entity;
    
    private void Update()
    {
        _entity.SetMoveX(_GetInputMoveX());
        _entity.SetMoveY(_GetInputMoveY());
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

    private float _GetInputMoveY()
    {
        float inputMoveY = 0f;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            inputMoveY = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveY = -1f;
        }

        return inputMoveY;
    }
}
