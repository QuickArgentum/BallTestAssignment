using UnityEngine;
using View;
using Zenject;

namespace DI
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private PoolablePrefabConfig[] poolablePrefabConfigs;
        
        public override void InstallBindings()
        {
            Container.Bind<ViewPool>().WithArguments(poolablePrefabConfigs);
        }
    }
}
