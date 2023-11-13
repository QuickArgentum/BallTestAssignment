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
        }

        public Transform ProjectilePoint => projectilePoint;
    }
}