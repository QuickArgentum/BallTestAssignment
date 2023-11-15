using System;
using DG.Tweening;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Component attached to projectiles controlling their size and collision logic
    /// </summary>
    public class ProjectileView : View
    {
        [SerializeField] private float scalePerEnergy;

        public event Action OnCollision;
        
        private Rigidbody _rigidbody;

        private float _energy;
        public float Energy
        {
            get => _energy;
            set
            {
                _energy = value;
                Transform.localScale = Vector3.one * value * scalePerEnergy;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision _)
        {
            if (!gameObject.activeSelf)
                return;

            OnCollision?.Invoke();
            Push();
        }

        public void Shoot(float velocity)
        {
            _rigidbody.velocity = Transform.forward * velocity;
        }

        public override void OnPop()
        {
            Energy = 0;
            _rigidbody.velocity = Vector3.zero;
        }

        public void PlayDisappearAnimation()
        {
            Transform.DOScale(Vector3.zero, 0.3f);
        }
    }
}