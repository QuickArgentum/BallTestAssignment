using UnityEngine;

namespace View
{
    public class PlayerView : View
    {
        [SerializeField] private Transform projectilePoint;
        [SerializeField] private Transform mesh;

        public float Scale
        {
            set => mesh.localScale = new Vector3(value, value, value);
            get => mesh.localScale.x;
        }

        public Transform ProjectilePoint => projectilePoint;
    }
}