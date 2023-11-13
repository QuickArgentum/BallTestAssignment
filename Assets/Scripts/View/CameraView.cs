﻿using DG.Tweening;
using UnityEngine;

namespace View
{
    public class CameraView : View
    {
        [SerializeField] private float fovAnimDelta;
        [SerializeField] private float fovDuration;
        
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
    }
}