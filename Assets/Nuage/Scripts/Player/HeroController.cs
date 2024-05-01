using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Entity")] 
    [SerializeField] private HeroEntity _entity;

    [Header("Sprite Renderer")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Header("Animator")] 
    [SerializeField] private Animator _animatorHero;
    
    private void Update()
    {
        _entity.SetMoveX(_GetInputMoveX());
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
}
