using DataHolder;
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
            Container.Bind<GameConfig>().FromScriptableObjectResource("GameConfig").AsSingle();
            Container.BindInstance(player);

        #if UNITY_EDITOR || UNITY_STANDALONE
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<MouseTouchManager>().AsSingle();
        #else
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<TouchManager>().AsSingle();
        #endif

            Container.Bind<GameFacade>().AsSingle();
            Container.Bind<GameStateHolder>().AsSingle();
            
            Container.Bind<ShootHandler>().AsSingle().NonLazy();
            Container.Bind<PlayerVisualsHandler>().AsSingle().NonLazy();
            Container.Bind<GameOverHandler>().AsSingle().NonLazy();
        }
    }
}
