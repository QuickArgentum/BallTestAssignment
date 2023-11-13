using Handlers;
using Input;
using UnityEngine;
using View;
using Zenject;

namespace DI
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private PoolablePrefabConfig[] poolablePrefabConfigs;
        [SerializeField] private PlayerView player;
        
        public override void InstallBindings()
        {
            Container.Bind<ViewPool>().AsSingle().WithArguments(poolablePrefabConfigs);

        #if UNITY_EDITOR || UNITY_STANDALONE
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<MouseTouchManager>().AsSingle();
        #else
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<TouchManager>().AsSingle();
        #endif

            Container.BindInstance(player);

            Container.Bind<ShootHandler>().AsSingle().NonLazy();
        }
    }
}
