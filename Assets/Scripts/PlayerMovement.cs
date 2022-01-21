using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _horizontalMove;
    private float _maxGroundDistanse = 0.01f;
    private bool _isGrounded = true;
    private bool _isAlife = true;
    private WaitForSeconds _deathDelay = new WaitForSeconds(5f);

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private const string Speed = "Speed";
    private const string Dead = "Death";
    private const string Jump = "Jump";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isAlife)
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
            _rigidbody.position += Vector2.right * _horizontalMove * _speed * Time.deltaTime;

            if (_horizontalMove != 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(_horizontalMove) / _horizontalMove, 1f, 1f);
            }

            if (Input.GetButton(Jump) & _isGrounded)
            {
                _rigidbody.velocity = Vector2.up * _jumpForce;
                _isGrounded = false;
                _animator.SetBool(Jump, true);
            }

            if (!_isGrounded)
            {
                GroundDetection();
            }
        }
    }

    public void Death()
    {
        _isAlife = false;
        _animator.SetTrigger(Dead);
        StartCoroutine(PlayerDeath());

    }

    private void GroundDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _maxGroundDistanse);

        for (int i = 0; i < colliders.Length; i++)
        {
            CompositeCollider2D tilemapCollider = colliders[i].GetComponent<CompositeCollider2D>();
            
            if (tilemapCollider != null)
            {
                _isGrounded = true;
                _animator.SetBool(Jump, false);
            }
        }
    }

    private IEnumerator PlayerDeath()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
        yield return _deathDelay;
        gameObject.SetActive(false);
    }
}
