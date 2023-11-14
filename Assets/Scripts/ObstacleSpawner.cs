using Const;
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
                Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
            obstacle.transform.parent = _container;
        }
    }
}