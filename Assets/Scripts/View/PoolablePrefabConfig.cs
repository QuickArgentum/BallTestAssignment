using System;
using Const;
using UnityEngine;

namespace View
{
    [Serializable]
    public struct PoolablePrefabConfig
    {
        public PrefabType type;
        public GameObject prefab;
    }
}