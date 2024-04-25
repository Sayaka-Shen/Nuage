using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroEntityTopDown : MonoBehaviour
{
    [Header("Physics")] 
    private Rigidbody2D _rigidbody;
    
    [Header("Horizontal Movements")]
    [SerializeField] private HeroHorizontalMovementsSettings _horizontalMovementsSettings;
    private float _moveX = 0f;
    private float _moveY = 0f;
    private float _horizontalSpeed = 0f;
    private float _verticalSpeed = 0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        _UpdateHorizontalSpeed();
        _UpdateVerticalSpeed();
        _ApplyHorizontalSpeed();
        _ApplyVerticalSpeed();
    }

    #region Functions Player Horizontal Movements
    
    public void SetMoveX(float directionX)
    {
        _moveX = directionX;
    }

    public void SetMoveY(float directionY)
    {
        _moveY = directionY;
    }
    
    private void _ApplyHorizontalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _horizontalSpeed * _moveX;
        _rigidbody.velocity = velocity;
    }
    
    private void _ApplyVerticalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.y = _verticalSpeed * _moveY;
        _rigidbody.velocity = velocity;
    }

    private void _UpdateHorizontalSpeed()
    {
        if (_moveX != 0f)
        {
            _Accelerate();
        }
    }

    private void _UpdateVerticalSpeed()
    {
        if (_moveY != 0f)
        {
            _Accelerate();
        }
    }

    private void _Accelerate()
    {
        _horizontalSpeed += _horizontalMovementsSettings.acceleration * Time.fixedDeltaTime;
        _verticalSpeed += _horizontalMovementsSettings.acceleration * Time.deltaTime;
        
        if (_horizontalSpeed > _horizontalMovementsSettings.speedMax)
        {
            _horizontalSpeed = _horizontalMovementsSettings.speedMax;
        }

        if (_verticalSpeed > _horizontalMovementsSettings.speedMax)
        {
            _verticalSpeed = _horizontalMovementsSettings.speedMax;
        }
    }

    private void _Decelerate()
    {
        _horizontalSpeed -= _horizontalMovementsSettings.deceleration * Time.fixedDeltaTime;
        _verticalSpeed -= _horizontalMovementsSettings.deceleration * Time.fixedDeltaTime;
        
        if (_horizontalSpeed < 0f)
        {
            _horizontalSpeed = 0f;
        }
        
        if (_verticalSpeed < 0f)
        {
            _verticalSpeed = 0f;
        }
    }
    
    #endregion 
}
