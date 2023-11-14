using System;
using UnityEngine;

namespace View
{
    public class ProjectileView : View
    {
        [SerializeField] private float scalePerEnergy;

        public event Action OnCollision;
        
        private Transform _transform;
        private Rigidbody _rigidbody;
        private float _energy;

        public float Energy => _energy;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision _)
        {
            if (!gameObject.activeSelf)
                return;

            OnCollision?.Invoke();
            Push();
        }

        public void Shoot(float velocity, float energy)
        {
            _rigidbody.velocity = _transform.forward * velocity;
            _energy = energy;
            _transform.localScale = Vector3.one * energy * scalePerEnergy;
        }

        public override void OnPush()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}