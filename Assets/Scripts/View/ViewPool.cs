using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace View
{
    public class ViewPool
    {
        private readonly Dictionary<PrefabType, IObjectPool<View>> _pools = new();
        private readonly Dictionary<PrefabType, GameObject> _prefabs = new();

        public ViewPool(PoolablePrefabConfig[] configs, DiContainer container)
        {
            foreach (var config in configs)
            {
                _prefabs[config.type] = config.prefab;

                View Create()
                {
                    var obj = container.InstantiatePrefab(_prefabs[config.type]);
                    var view = obj.GetComponent<View>();
                    view.Pool = this;
                    view.Type = config.type;
                    return view;
                }

                _pools[config.type] = new ObjectPool<View>(Create, OnGet, OnRelease);
            }
        }

        private void OnGet(View view)
        {
            view.gameObject.SetActive(true);
            view.OnPop();
        }
        
        private void OnRelease(View view)
        {
            view.gameObject.SetActive(false);
            view.OnPush();
        }

        public T Pop<T>(PrefabType type, Vector3 position, Quaternion rotation) where T : View
        {
            var view = _pools[type].Get();
            view.Transform.SetPositionAndRotation(position, rotation);
            return view as T;
        }

        public void Push(View view)
        {
            _pools[view.Type].Release(view);
        }
    }
}