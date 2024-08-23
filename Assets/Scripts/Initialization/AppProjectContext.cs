using Assets.Scripts.Core.Scenes;
using Zenject;

namespace Assets.Scripts.Initialization
{
    public class AppProjectContext : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneTransition();
        }

        private void BindSceneTransition()
        {
            Container.Bind<SceneTransition>().AsSingle().NonLazy();
        }
    }
}
