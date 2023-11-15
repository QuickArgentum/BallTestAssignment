using Data;
using DataHolder;
using DG.Tweening;
using Input;
using UnityEngine;
using View;

namespace Handlers
{
    /// <summary>
    /// Responsible for detecting when the player wins the game and playing the victory animation
    /// </summary>
    public class VictoryHandler
    {
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly PlayerView _player;
        private readonly DestinationView _destination;
        private readonly GameStateHolder _gameStateHolder;
        private readonly CameraView _camera;
        private readonly ITouchManager _touchManager;

        private int _infectedCount;
        private float _lastEnergyValue;

        public VictoryHandler(ObstacleSpawner obstacleSpawner, PlayerView player, DestinationView destination,
            GameStateHolder gameStateHolder, CameraView camera, ITouchManager touchManager)
        {
            _obstacleSpawner = obstacleSpawner;
            _player = player;
            _destination = destination;
            _gameStateHolder = gameStateHolder;
            _camera = camera;
            _touchManager = touchManager;

            _gameStateHolder.OnGameStateUpdated += OnGameStateUpdated;
        }

        private void OnGameStateUpdated(GameState value)
        {
            switch (value)
            {
                case GameState.Playing:
                    _obstacleSpawner.OnObstacleInfected += OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed += OnObstacleDestroyed;
                    _touchManager.OnTapEnd += CheckVictoryCondition;
                    break;
                
                case GameState.WinAnimationShowing:
                    _obstacleSpawner.OnObstacleInfected -= OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed -= OnObstacleDestroyed;
                    _touchManager.OnTapEnd -= CheckVictoryCondition;
                    DOTween.Sequence()
                        .Append(_player.CreateVictoryTween(_destination.Transform.position))
                        .AppendCallback(() =>
                        {
                            _destination.PlayVictoryAnimation();
                            _camera.PlayShake(0.25f);
                            _gameStateHolder.GameState = GameState.GameOverScreenShowing;
                        })
                        .Play();
                    break;
                
                case GameState.GameOverScreenShowing:
                    _obstacleSpawner.OnObstacleInfected -= OnObstacleInfected;
                    _obstacleSpawner.OnObstacleDestroyed -= OnObstacleDestroyed;
                    _touchManager.OnTapEnd -= CheckVictoryCondition;
                    break;
            }
        }

        private void OnObstacleInfected() => _infectedCount++;

        private void OnObstacleDestroyed()
        {
            _infectedCount--;
            if (_infectedCount <= 0)
                CheckVictoryCondition();
        }

        private void CheckVictoryCondition()
        {
            if (PlayerWon())
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