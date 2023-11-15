using System;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Data structure to register prefabs as poolable in the main game installer
    /// </summary>
    [Serializable]
    public struct PoolablePrefabConfig
    {
        public PrefabType type;
        public GameObject prefab;
    }
}