using System;
using Const;
using UnityEngine;

namespace View
{
    public class ProjectileView : View
    {
        [SerializeField] private float scalePerEnergy;

        public event Action OnCollision;
        
        private Transform _transform;
        private Rigidbody _rigidbody;

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
            _transform.localScale = Vector3.one * energy;
        }

        public override void OnPush()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}