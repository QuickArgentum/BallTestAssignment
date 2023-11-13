using UnityEngine;

namespace View
{
    public class ProjectileView : View
    {
        [SerializeField] private float velocity;

        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Push();
        }

        public void Shoot()
        {
            _rigidbody.velocity = _transform.forward * velocity;
        }

        public override void OnPush()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}