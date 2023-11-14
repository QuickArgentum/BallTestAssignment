using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct PoolablePrefabConfig
    {
        public PrefabType type;
        public GameObject prefab;
    }
}