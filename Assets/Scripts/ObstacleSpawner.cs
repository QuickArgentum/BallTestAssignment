using System.Collections;
using Data;
using UnityEngine;
using View;
using Zenject;
using Random = UnityEngine.Random;

public class ObstacleSpawner : IInitializable
{
    private readonly Transform _container;
    private readonly ViewPool _viewPool;
    private readonly GameConfig _gameConfig;

    public ObstacleSpawner(Transform container, ViewPool viewPool, GameConfig gameConfig)
    {
        _container = container;
        _viewPool = viewPool;
        _gameConfig = gameConfig;
    }

    void IInitializable.Initialize()
    {
        var halfWidth = _gameConfig.obstacleFieldWidth / 2;
        var halfHeight = _gameConfig.obstacleFieldHeight / 2;
        var bias = _gameConfig.obstacleNoiseBias;
        var containerPosition = _container.position;
        
        for (var i = 0; i < _gameConfig.obstacleGenerationAttempts; i++)
        {
            var position = new Vector3(Random.Range(-halfWidth, halfWidth), 0, Random.Range(-halfHeight, halfHeight));
            var chance = bias + Mathf.PerlinNoise(position.x * _gameConfig.obstacleNoiseScale,
                position.z * _gameConfig.obstacleNoiseScale) * (1 - bias);
            if (chance >= Random.value)
                continue;

            var obstacle = _viewPool.Pop<ObstacleView>(PrefabType.Obstacle, position + containerPosition,
                Quaternion.identity);

            void OnInfected()
            {
                obstacle.OnInfected -= OnInfected;
                obstacle.StartCoroutine(InfectedCoroutine(obstacle));
            }

            obstacle.OnInfected += OnInfected;
            obstacle.transform.parent = _container;
        }
    }

    private IEnumerator InfectedCoroutine(ObstacleView obstacle)
    {
        yield return new WaitForSeconds(_gameConfig.obstacleDeathDelay);
        _viewPool.Pop<OneShotParticlesView>(PrefabType.ObstacleDeathEffect, obstacle.Transform.position,
            Quaternion.Euler(-90, 0, 0));
        obstacle.Push();
    }
}