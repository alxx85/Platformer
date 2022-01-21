using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;

    private void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, _moveSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minPosition, _maxPosition), transform.position.y, transform.position.z);
    }
}
