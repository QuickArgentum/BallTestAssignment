using Data;
using DataHolder;
using Handlers;
using Input;
using UnityEngine;
using View;
using Zenject;

namespace DI
{
    /// <summary>
    /// Main game installer this object may be viewed as the entry point of the application and this is where
    /// Zenject installs all the types into the DI container
    /// </summary>
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private PoolablePrefabConfig[] poolablePrefabConfigs;
        [SerializeField] private Transform obstacleContainer;

        public override void InstallBindings()
        {
            Container.Bind<ViewPool>().AsSingle().WithArguments(poolablePrefabConfigs);
            Container.Bind<GameConfig>().FromScriptableObjectResource("GameConfig").AsSingle();

        #if UNITY_EDITOR || UNITY_STANDALONE
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<MouseTouchManager>().AsSingle();
        #else
            Container.Bind(typeof(ITouchManager), typeof(ITickable)).To<TouchManager>().AsSingle();
        #endif

            Container.BindInterfacesAndSelfTo<GameFacade>().AsSingle();
            Container.Bind<GameStateHolder>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleSpawner>().AsSingle().WithArguments(obstacleContainer);
            
            Container.Bind<ShootHandler>().AsSingle().NonLazy();
            Container.Bind<PlayerVisualsHandler>().AsSingle().NonLazy();
            Container.Bind<GameOverHandler>().AsSingle().NonLazy();
            Container.Bind<GameTransitionHandler>().AsSingle().NonLazy();
            Container.Bind<VictoryHandler>().AsSingle().NonLazy();
        }
    }
}
