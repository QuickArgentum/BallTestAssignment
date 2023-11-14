using System;
using UnityEngine;

namespace View
{
    public class ObstacleView : View
    {
        [SerializeField] private Color infectedColor;
        [SerializeField] private new Renderer renderer;

        private Material _material;
        private bool _isInfected;
        
        public event Action OnInfected;

        private void Awake()
        {
            _material = renderer.material;
        }

        public void Infect()
        {
            if (_isInfected)
                return;

            _isInfected = true;
            _material.color = infectedColor;
            OnInfected?.Invoke();
        }
    }
}