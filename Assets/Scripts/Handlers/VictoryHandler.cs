using Data;
using DataHolder;
using DG.Tweening;
using UnityEngine;
using View;

namespace Handlers
{
    public class VictoryHandler
    {
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly PlayerView _player;
        private readonly DestinationView _destination;
        private readonly GameStateHolder _gameStateHolder;

        private int _infectedCount;

        public VictoryHandler(ObstacleSpawner obstacleSpawner, PlayerView player, DestinationView destination,
            GameStateHolder gameStateHolder)
        {
            _obstacleSpawner = obstacleSpawner;
            _player = player;
            _destination = destination;
            _gameStateHolder = gameStateHolder;

            _gameStateHolder.OnGameStateUpdated += OnGameStateUpdated;
        }

        private void OnGameStateUpdated(GameState value)
        {
            switch (value)
            {
                case GameState.Playing:
                    _obstacleSpawner.OnObstacleInfected += OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed += OnObstacleDestroyed;
                    break;
                
                case GameState.WinAnimationShowing:
                    _obstacleSpawner.OnObstacleInfected -= OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed -= OnObstacleDestroyed;
                    DOTween.Sequence()
                        .Append(_player.CreateVictoryTween(_destination.Transform.position))
                        .AppendCallback(() => _gameStateHolder.GameState = GameState.GameOverScreenShowing)
                        .Play();
                    break;
                
                case GameState.GameOverScreenShowing:
                    _obstacleSpawner.OnObstacleInfected -= OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed -= OnObstacleDestroyed;
                    break;
            }
        }

        private void OnObstacleInfected() => _infectedCount++;

        private void OnObstacleDestroyed()
        {
            _infectedCount--;
            if (_infectedCount <= 0 && PlayerWon())
                _gameStateHolder.GameState = GameState.WinAnimationShowing;
        }

        private bool PlayerWon()
        {
            if (Physics.SphereCast(_player.Transform.position, _player.Scale / 2,
                    _destination.Transform.position - _player.Transform.position,
                    out var hit, 20.0f, Layers.VictoryMask))
            {
                return hit.transform.gameObject.layer == Layers.Destination;
            }

            return true;
        }
    }
}