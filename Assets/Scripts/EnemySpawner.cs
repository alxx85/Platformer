using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMover _prefab;
    [SerializeField] private bool _isLoop = true;
    [SerializeField] private bool _spawnInTurn = true;
    [SerializeField] private float _spawnDelay = 2f;

    private List<EnemySpawnPoint> _spawnPoints;
    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);
        _spawnPoints = new List<EnemySpawnPoint>(GetComponentsInChildren<EnemySpawnPoint>());
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        do
        {
            foreach (EnemySpawnPoint point in _spawnPoints)
            {
                EnemyMover newEnemy = Instantiate(_prefab, point.transform.position, Quaternion.identity, point.transform);
                newEnemy.SetEndPoint(point.EndPosition);
                newEnemy.StartMoving();
                if (_spawnInTurn)
                {
                    yield return _delay;
                }
            }
        }
        while (_isLoop);
    }
}
