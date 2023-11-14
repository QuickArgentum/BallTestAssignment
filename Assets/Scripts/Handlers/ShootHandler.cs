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

        private float _nextFireTime;
        private ProjectileView _projectile;
        private float _holdTime;
        private bool _autoStop;

        public ShootHandler(PlayerView player, ViewPool viewPool, ITouchManager touchManager,
            GameStateHolder gameStateHolder, GameConfig gameConfig, TickableManager tickableManager)
        {
            _player = player;
            _viewPool = viewPool;
            _touchManager = touchManager;
            _gameStateHolder = gameStateHolder;
            _gameConfig = gameConfig;
            _tickableManager = tickableManager;

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
                
                case GameState.GameOverScreenShowing:
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