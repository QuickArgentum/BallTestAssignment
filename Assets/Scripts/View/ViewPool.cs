using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Pool;

namespace View
{
    public class ViewPool
    {
        private readonly Dictionary<PrefabType, IObjectPool<GameObject>> _pools = new();
        private readonly Dictionary<PrefabType, GameObject> _prefabs = new();

        public ViewPool(PoolablePrefabConfig[] configs)
        {
            foreach (var config in configs)
            {
                _prefabs[config.type] = config.prefab;

                GameObject Create()
                {
                    var obj = Object.Instantiate(_prefabs[config.type]);
                    var view = obj.GetComponent<View>();
                    view.Pool = this;
                    view.Type = config.type;
                    return obj;
                }

                _pools[config.type] = new ObjectPool<GameObject>(Create, OnGet, OnRelease);
            }
        }

        private void OnGet(GameObject obj)
        {
            obj.SetActive(true);
            obj.GetComponent<View>().OnPop();
        }
        
        private void OnRelease(GameObject obj)
        {
            obj.SetActive(false);
            obj.GetComponent<View>().OnPush();
        }

        public T Pop<T>(PrefabType type, Vector3 position, Quaternion rotation)
        {
            var obj = _pools[type].Get();
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj.GetComponent<T>();
        }

        public void Push(View view)
        {
            _pools[view.Type].Release(view.gameObject);
        }
    }
}