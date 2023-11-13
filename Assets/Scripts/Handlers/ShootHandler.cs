using Const;
using DataHolder;
using Input;
using View;

namespace Handlers
{
    public class ShootHandler
    {
        private readonly PlayerView _player;
        private readonly ViewPool _viewPool;
        private readonly GameStateHolder _gameStateHolder;
        private readonly GameConfig _gameConfig;

        public ShootHandler(PlayerView player, ViewPool viewPool, ITouchManager touchManager,
            GameStateHolder gameStateHolder, GameConfig gameConfig)
        {
            _player = player;
            _viewPool = viewPool;
            _gameStateHolder = gameStateHolder;
            _gameConfig = gameConfig;

            touchManager.OnTapEnd += OnTapEnd;
        }

        private void OnTapEnd()
        {
            var point = _player.ProjectilePoint;
            var projectile = _viewPool.Pop<ProjectileView>(PrefabType.Projectile, point.position, point.rotation);
            projectile.Shoot(_gameConfig.projectileVelocity);

            _gameStateHolder.PlayerEnergy -= _gameConfig.energyPerShot;
        }
    }
}