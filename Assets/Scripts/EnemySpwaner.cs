using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Level _level;
    [SerializeField] private float _waveDelay = 2f;
    private readonly List<Enemy> _spawnedEnemies = new();

    private int _wave = 1;
    private IEnumerator _coroutine;

    private void FixedUpdate()
    {
        if (_spawnedEnemies.Count > 0 || _coroutine != null)
        {
            return;
        }
        _coroutine = SpawnWaveCoroutine();
        StartCoroutine(_coroutine);        
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        yield return new WaitForSeconds(_waveDelay);
        for (var i = 0; i < _wave; i++)
        {
            SpawnRandomEnemy();
        }
        _wave++;
        _coroutine = null;
    }

    private void SpawnRandomEnemy()
    {
        var bounds = _level.GetLevelBounds();
        var positionX = Random.Range(bounds.min.x, bounds.max.x);
        var positionY = Random.Range(bounds.min.y, bounds.max.y);
        var position = new Vector3(positionX, positionY, 0f);
        var rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        var newEnemy = Instantiate(_enemyPrefab, position, rotation, null);
        newEnemy.OnDestroyEvent += OnEnemyDestroy;
        _spawnedEnemies.Add(newEnemy);
    }

    private void OnEnemyDestroy(Enemy enemy)
    {
        enemy.OnDestroyEvent -= OnEnemyDestroy;
        _spawnedEnemies.Remove(enemy);
    }

    private void OnDestroy()
    {
        if (_coroutine == null)
        {
            return;
        }
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
