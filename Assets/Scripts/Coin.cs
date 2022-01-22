using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;

    private float _moveY = 0.2f;
    private float _duration = .5f;

    private void Start()
    {
        transform.DOMoveY(transform.position.y + _moveY, _duration, false).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCoins>(out PlayerCoins player))
        {
            player.AddCoins(_value);
            Destroy(gameObject);
        }
    }
}
