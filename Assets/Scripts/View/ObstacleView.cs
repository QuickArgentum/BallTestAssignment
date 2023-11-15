using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View
{
    /// <summary>
    /// Component attached to obstacles. Randomizes some of their transform values on creation for visual fidelity
    /// </summary>
    public class ObstacleView : View
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Transform view;
        [SerializeField] private Color infectedColor;
        [SerializeField] private float infectionTime;
        [SerializeField] [Range(0, 1)] private float scaleVariance;
        [SerializeField] [Range(0, 1)] private float tiltVariance;

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
            _material.DOColor(infectedColor, infectionTime);
            OnInfected?.Invoke();
        }

        public override void OnPop()
        {
            view.localScale = Vector3.one * Random.Range(1 - scaleVariance, 1);
            var randomVec2 = Random.insideUnitCircle;
            var randomAxis = Vector3.Lerp(new Vector3(randomVec2.x, 0, randomVec2.y), Vector3.up,
                Random.Range(1 - tiltVariance, 1));
            view.localRotation = Quaternion.AngleAxis(Random.Range(0, 360), randomAxis);
        }
    }
}