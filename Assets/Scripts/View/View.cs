using Const;
using UnityEngine;

namespace View
{
    public class View : MonoBehaviour
    {
        public ViewPool Pool { get; set; }
        public PrefabType Type { get; set; }

        public void Push()
        {
            Pool.Push(this);
        }
    }
}