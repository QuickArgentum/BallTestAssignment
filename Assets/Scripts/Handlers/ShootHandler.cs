using Data;
using DataHolder;
using Input;
using UnityEngine;
using View;
using Zenject;

namespace Handlers
{
    public class ShootHandler : ITickable
    {
        private readonly PlayerView _player;
        private readonly ViewPool _viewPool;
        private readonly ITouchManager _touchManager;
        private readonly GameStateHolder _gameStateHolder;
        private readonly GameConfig _gameConfig;
        private readonly TickableManager _tickableManager;
        private readonly CameraView _camera;

        private float _nextFireTime;
        private ProjectileView _projectile;
        private float _holdTime;
        private bool _autoStop;
        private bool _isTicking;

        public ShootHandler(PlayerView player, ViewPool viewPool, ITouchManager touchManager,
            GameStateHolder gameStateHolder, GameConfig gameConfig, TickableManager tickableManager, CameraView camera)
        {
            _player = player;
            _viewPool = viewPool;
            _touchManager = touchManager;
            _gameStateHolder = gameStateHolder;
            _gameConfig = gameConfig;
            _tickableManager = tickableManager;
            _camera = camera;

            _gameStateHolder.OnGameStateUpdated += OnGameStateUpdated;
        }

        private void OnGameStateUpdated(GameState value)
        {
            switch (value)
            {
                case GameState.Playing:
                    _touchManager.OnTapStart += OnTapStart;
                    _touchManager.OnTapEnd += OnTapEnd;
                    break;
                
                case GameState.WinAnimationShowing:
                case GameState.GameOverScreenShowing:
                    if (_isTicking)
                        _tickableManager.Remove(this);
                    
                    if(_projectile != null)
                        _projectile.PlayDisappearAnimation();
                    
                    _isTicking = false;
                    _touchManager.OnTapStart -= OnTapStart;
                    _touchManager.OnTapEnd -= OnTapEnd;
                    break;
            }
        }

        private void OnTapStart()
        {
            if (_autoStop || _projectile != null || Time.time < _nextFireTime)
                return;
            
            _holdTime = 0;
            
            var point = _player.ProjectilePoint;
            var projectile = _viewPool.Pop<ProjectileView>(PrefabType.Projectile, point.position, point.rotation);
            void OnCollision()
            {
                projectile.OnCollision -= OnCollision;
                CreateExplosion(projectile);
            }
            projectile.OnCollision += OnCollision;

            _projectile = projectile;
            _tickableManager.Add(this);
            _isTicking = true;
        }

        private void OnTapEnd()
        {
            if (_projectile == null)
                return;
            
            if (_projectile.Energy >= _gameConfig.minShotEnergy)
                LaunchProjectile();
            else
                _autoStop = true;
        }

        private void LaunchProjectile()
        {
            _isTicking = false;
            _tickableManager.Remove(this);
            _projectile.Shoot(_gameConfig.projectileVelocity);

            _nextFireTime = Time.time + _gameConfig.refireInterval;
            _projectile = null;
        }

        private void CreateExplosion(ProjectileView projectile)
        {
            var position = projectile.Transform.position;
            var explosion = _viewPool.Pop<ExplosionView>(PrefabType.Explosion, position, Quaternion.identity);
            explosion.Explode(projectile.Energy * _gameConfig.explosionScalePerEnergy);

            _viewPool.Pop<ProjectileBreakParticlesView>(PrefabType.ProjectileBreakEffect, position, Quaternion.identity);
            _camera.PlayShake(projectile.Energy);
        }

        void ITickable.Tick()
        {
            _holdTime += Time.deltaTime;
            var energyCandidate = 
                Time.deltaTime * _gameConfig.energyTransferRate * _gameConfig.energyTransferCurve.Evaluate(_holdTime);
            var energyActual = Mathf.Min(energyCandidate, _gameConfig.maxShotEnergy - _projectile.Energy);
            if (energyActual <= 0)
                return;
            
            _projectile.Energy += energyActual;
            _gameStateHolder.PlayerEnergy -= energyActual;

            if (_autoStop && _projectile.Energy >= _gameConfig.minShotEnergy)
            {
                _autoStop = false;
                LaunchProjectile();
            }
        }
    }
}