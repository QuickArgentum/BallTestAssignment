using UnityEngine;
using Zenject;

namespace View
{
    /// <summary>
    /// Component attached to particle systems which spawn when the projectile hits something and is destroyed
    /// </summary>
    public class ProjectileBreakParticlesView : OneShotParticlesView
    {
        [Inject] private CameraView _camera;
        
        public override void OnPop()
        {
            base.OnPop();
            Transform.rotation = Quaternion.LookRotation(_camera.Transform.position - Transform.position);
        }
    }
}