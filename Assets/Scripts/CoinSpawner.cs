using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private bool _spawnInTurn = true;
    [SerializeField] private float _spawnDelay = 1f;

    private List<CoinSpawnPoint> _spawnPoints;
    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);
        _spawnPoints = new List<CoinSpawnPoint>(GetComponentsInChildren<CoinSpawnPoint>());
        StartCoroutine(Spawn());

    }

    private IEnumerator Spawn()
    {
        foreach (CoinSpawnPoint point in _spawnPoints)
        {
            Coin newCoin = Instantiate(_coinPrefab, point.transform.position, Quaternion.identity, point.transform);
            
            if (_spawnInTurn)
            {
                yield return _delay;
            }
        }

    }
}
