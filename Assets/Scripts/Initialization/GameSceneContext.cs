using UnityEngine;
using Zenject;

namespace Assets.Scripts.Initialization
{
    public class GameSceneContext : MonoInstaller
    {
        [SerializeField] private NetworkInitializer _networkInitializerPrefab;

        public override void InstallBindings()
        {
            var initializer = Container.InstantiatePrefabForComponent<NetworkInitializer>(_networkInitializerPrefab);
            Container.Bind<NetworkInitializer>().FromInstance(initializer).AsSingle().NonLazy();
        }
    }
}