using UnityEngine;

public class HeroEntity : MonoBehaviour
{
    [Header("Physics")] 
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [Header("Horizontal Movements")]
    [SerializeField] private HeroHorizontalMovementsSettings _horizontalMovementsSettings;
    private float _moveX = 0f;
    private float _horizontalSpeed = 0f;
    
    private void FixedUpdate()
    {
        _UpdateHorizontalSpeed();
        _ApplyHorizontalSpeed();
    }

    #region Functions Player Horizontal Movements
    
    public void SetMoveX(float directionX)
    {
        _moveX = directionX;
    }

    private void _ApplyHorizontalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _horizontalSpeed * _moveX;
        _rigidbody.velocity = velocity;
    }

    private void _UpdateHorizontalSpeed()
    {
        if (_moveX != 0f)
        {
            _Accelerate();
        }
        else
        {
            _Decelerate();
        }
    }

    private void _Accelerate()
    {
        _horizontalSpeed += _horizontalMovementsSettings.acceleration * Time.fixedDeltaTime;
        
        if (_horizontalSpeed > _horizontalMovementsSettings.speedMax)
        {
            _horizontalSpeed = _horizontalMovementsSettings.speedMax;
        }
    }

    private void _Decelerate()
    {
        _horizontalSpeed -= _horizontalMovementsSettings.deceleration * Time.fixedDeltaTime;

        if (_horizontalSpeed < 0f)
        {
            _horizontalSpeed = 0f;
        }
    }
    
    #endregion
}
