using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _endPoint;
    private float _rotateDelay = 0.5f;
    private Vector3 _forward = new Vector3(0, 180, 0);
    private Vector3 _back = new Vector3(0, 0, 0);

    public void SetEndPoint(Vector3 position)
    {
        _endPoint = position;
    }

    public void StartMoving()
    {
        float delay = Vector3.Distance(transform.position, _endPoint) / _speed;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(_forward, 0));

        sequence.Append(transform.DOMove(_endPoint, delay).SetEase(Ease.Linear).SetDelay(_rotateDelay));

        sequence.Append(transform.DORotate(_back, 0).SetDelay(_rotateDelay));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.Death();
        }
    }
}
