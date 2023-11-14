using Const;
using DataHolder;
using Input;
using UnityEngine;
using View;

namespace Handlers
{
    public class ShootHandler
    {
        private readonly PlayerView _player;
        private readonly ViewPool _viewPool;
        private readonly ITouchManager _touchManager;
        private readonly GameStateHolder _gameStateHolder;
        private readonly GameConfig _gameConfig;

        public ShootHandler(PlayerView player, ViewPool viewPool, ITouchManager touchManager,
            GameStateHolder gameStateHolder, GameConfig gameConfig)
        {
            _player = player;
            _viewPool = viewPool;
            _touchManager = touchManager;
            _gameStateHolder = gameStateHolder;
            _gameConfig = gameConfig;

            _gameStateHolder.OnGameStateUpdated += OnGameStateUpdated;
        }

        private void OnGameStateUpdated(GameState value)
        {
            switch (value)
            {
                case GameState.Playing:
                    _touchManager.OnTapEnd += OnTapEnd;
                    break;
                
                case GameState.GameOverScreenShowing:
                    _touchManager.OnTapEnd -= OnTapEnd;
                    break;
            }
        }

        private void OnTapEnd()
        {
            var point = _player.ProjectilePoint;
            var projectile = _viewPool.Pop<ProjectileView>(PrefabType.Projectile, point.position, point.rotation);
            
            void OnCollision()
            {
                projectile.OnCollision -= OnCollision;
                CreateExplosion(projectile);
            }

            projectile.OnCollision += OnCollision;
            projectile.Shoot(_gameConfig.projectileVelocity, _gameConfig.energyPerShot);

            _gameStateHolder.PlayerEnergy -= _gameConfig.energyPerShot;
        }

        private void CreateExplosion(ProjectileView projectile)
        {
           var explosion = _viewPool.Pop<ExplosionView>(PrefabType.Explosion, projectile.transform.position, Quaternion.identity);
           explosion.Explode(projectile.Energy * _gameConfig.explosionScalePerEnergy);
        }
    }
}