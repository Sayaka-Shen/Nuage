using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControllerTopDown : MonoBehaviour
{
    [Header("Entity")] 
    [SerializeField] private HeroEntityTopDown _entity;
    
    [Header("Sprite Renderer")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Header("Animator")] 
    [SerializeField] private Animator _animatorHero;
    
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
            _spriteRenderer.flipX = true;
            _animatorHero.SetBool("IsWalkingBackward", true);
            inputMoveX = -1f;
        }
        else
        {
            _animatorHero.SetBool("IsWalkingBackward", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            _animatorHero.SetBool("IsWalking", true);
            inputMoveX = 1f;
        }
        else
        {
            _animatorHero.SetBool("IsWalking", false);
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
