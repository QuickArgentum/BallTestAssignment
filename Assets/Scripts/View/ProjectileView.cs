using UnityEngine;

namespace View
{
    public class ProjectileView : View
    {
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!gameObject.activeSelf)
                return;
            
            Push();
        }

        public void Shoot(float velocity)
        {
            _rigidbody.velocity = _transform.forward * velocity;
        }

        public override void OnPush()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}