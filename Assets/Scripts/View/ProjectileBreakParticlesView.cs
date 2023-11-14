using UnityEngine;
using Zenject;

namespace View
{
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