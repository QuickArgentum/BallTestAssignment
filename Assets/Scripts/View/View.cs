using Data;
using UnityEngine;

namespace View
{
    public class View : MonoBehaviour
    {
        private Transform _transform;
        public Transform Transform => _transform == null ? _transform = transform : _transform;
        
        public ViewPool Pool { get; set; }
        public PrefabType Type { get; set; }

        public void Push()
        {
            Pool.Push(this);
        }
        
        public virtual void OnPush() { }

        public virtual void OnPop() { }
    }
}