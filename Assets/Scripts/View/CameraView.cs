using DG.Tweening;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Component attached to camera responsible for animating FOV and shaking it
    /// </summary>
    public class CameraView : View
    {
        [SerializeField] private float fovAnimDelta;
        [SerializeField] private float fovDuration;
        [SerializeField] private float shakeDurationPerEnergy;
        [SerializeField] private float shakeStrengthPerEnergy;
        
        private Camera _camera;
        private float _fov;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _fov = _camera.fieldOfView;
        }

        public Tween CreateIntroTween()
        {
            _camera.fieldOfView = _fov + fovAnimDelta;
            return _camera.DOFieldOfView(_fov, fovDuration);
        }

        public Tween CreateOutroTween()
        {
            return _camera.DOFieldOfView(_fov + fovAnimDelta, fovDuration);
        }

        public void PlayShake(float energy)
        {
            Transform.DOShakeRotation(shakeDurationPerEnergy * energy, shakeStrengthPerEnergy * energy, 20);
        }
    }
}