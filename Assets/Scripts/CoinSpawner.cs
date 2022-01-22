using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private bool _spawnInTurn = true;
    [SerializeField] private float _delay = 1f;

    private List<CoinSpawnPoint> _spawnPoints;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        _spawnPoints = new List<CoinSpawnPoint>(GetComponentsInChildren<CoinSpawnPoint>());
        StartCoroutine(Spawn());

    }

    private IEnumerator Spawn()
    {
        foreach (CoinSpawnPoint point in _spawnPoints)
        {
            Coin newCoin = Instantiate(_prefab, point.transform.position, Quaternion.identity, point.transform);
            
            if (_spawnInTurn)
            {
                yield return _wait;
            }
        }

    }
}
