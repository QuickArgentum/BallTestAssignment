using Data;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Generic base class to create components which control poolable prefabs
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private Transform _transform;
        /// <summary>
        /// Cached transform reference
        /// </summary>
        public Transform Transform => _transform == null ? _transform = transform : _transform;
        
        public ViewPool Pool { get; set; }
        public PrefabType Type { get; set; }

        /// <summary>
        /// "Destroy" the object by returning it into the pool
        /// </summary>
        public void Push()
        {
            Pool.Push(this);
        }
        
        /// <summary>
        /// Called when the object is "destroyed" - returned to the pool
        /// </summary>
        public virtual void OnPush() { }

        /// <summary>
        /// Called when the object is created
        /// </summary>
        public virtual void OnPop() { }
    }
}