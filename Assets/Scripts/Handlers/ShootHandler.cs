using Const;
using Input;
using View;

namespace Handlers
{
    public class ShootHandler
    {
        private readonly PlayerView _player;
        private readonly ViewPool _viewPool;

        public ShootHandler(PlayerView player, ViewPool viewPool, ITouchManager touchManager)
        {
            _player = player;
            _viewPool = viewPool;

            touchManager.OnTapEnd += OnTapEnd;
        }

        private void OnTapEnd()
        {
            var point = _player.ProjectilePoint;
            var projectile = _viewPool.Pop<ProjectileView>(PrefabType.Projectile, point.position, point.rotation);
            projectile.Shoot();
        }
    }
}