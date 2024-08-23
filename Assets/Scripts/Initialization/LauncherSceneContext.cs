using Assets.Scripts.NetworkPhoton;
using Assets.Scripts.Save;
using Assets.Scripts.Settings;
using Assets.Scripts.UI.Presenters;
using Assets.Scripts.UI.Transition;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Initialization
{
    public class LauncherSceneContext : MonoInstaller
    {
        [SerializeField] private Presenter _presenter;
        [SerializeField] private SettingsPresenter _settingsPresenter;
        [SerializeField] private Launcher _launcher;
        [SerializeField] private PanelTransition _panelTransition;
        [SerializeField] private RoomList _roomList;
        [SerializeField] private PlayerList _playerList;
        [SerializeField] private LogFeedback _logFeedback;
        [SerializeField] private SettingsDataProvider _settingsProvider;
        [SerializeField] private EntryPoint _entryPoint;

        public override void InstallBindings()
        {
            BindPresenter();
            BindPanelTransition();
            BindRoomList();
            BindPlayerList();
            BindLogfeedback();
            BindLauncher();
            BindSaveSystem();
            BindEntryPoint();
            BindAppSettings();
        }

        private void BindPresenter()
        {
            Container.Bind(typeof(IMenuPresenter), typeof(INetworkPresenter)).FromInstance(_presenter).AsSingle().NonLazy();
            Container.Bind(typeof(ISettingsPresenter)).FromInstance(_settingsPresenter).AsSingle().NonLazy();
        }

        private void BindLogfeedback()
        {
            Container.Bind<LogFeedback>().FromInstance(_logFeedback).AsSingle().NonLazy();
        }

        private void BindLauncher()
        {
            Container.Bind<Launcher>().FromInstance(_launcher).AsSingle().NonLazy();
        }

        private void BindPanelTransition()
        {
            Container.Bind<PanelTransition>().FromInstance(_panelTransition).AsSingle().NonLazy();
        }

        private void BindRoomList()
        {
            Container.Bind<RoomList>().FromInstance(_roomList).AsSingle().NonLazy();
        }

        private void BindPlayerList()
        {
            Container.Bind<PlayerList>().FromInstance(_playerList).AsSingle().NonLazy();
        }

        private void BindSaveSystem()
        {
            Container.Bind<ISaveSystem>().To<BinarySaveSystem>().AsSingle().NonLazy();
        }

        private void BindAppSettings()
        {
            Container.Bind<GraphicsSettings>().AsSingle().NonLazy();
            Container.Bind<ControlSettings>().AsSingle().NonLazy();
            Container.Bind<AppSettingsSetter>().AsSingle().NonLazy();
            Container.Bind<SettingsDataProvider>().FromInstance(_settingsProvider).AsSingle();
        }

        private void BindEntryPoint()
        {
            Container.Bind<EntryPoint>().FromInstance(_entryPoint).AsSingle().NonLazy();
        }
    }
}
