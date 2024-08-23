using Assets.Scripts.UI.Panels;
using Assets.Scripts.UI.Presenters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Transition
{
    public class PanelTransition : UIElement
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private LoadingPanel _loadingPanel;
        [SerializeField] private FindRoomPanel _findRoomPanel;
        [SerializeField] private CreatingRoomPanel _creatingRoomPanel;
        [SerializeField] private RoomPanel _roomPanel;
        [SerializeField] private SettingsPanel _settginsPanel;
        [SerializeField] private TeamVsTeamPanel _teamVsTeamPanel;

        private Dictionary<PanelType, UIPanel> _panels;
        private UIPanel _currentPanel;
        private UIPanel _previousPanel;

        private IMenuPresenter _presenter;

        [Inject]
        private void Construct(IMenuPresenter presenter)
        {
            _presenter = presenter;

            _panels = new Dictionary<PanelType, UIPanel>
            {
                { PanelType.Main, _mainPanel },
                { PanelType.Loading, _loadingPanel },
                { PanelType.FindRoom, _findRoomPanel },
                { PanelType.CreatingRoom, _creatingRoomPanel },
                { PanelType.Room, _roomPanel },
                { PanelType.Settings, _settginsPanel },
                { PanelType.TeamVsTeam, _teamVsTeamPanel },
            };

            _currentPanel = _mainPanel;
            _previousPanel = _currentPanel;

            AddListeneres();
        }

        public void SwitchPanel(PanelType type)
        {
            if (_panels.ContainsKey(type))
            {
                if (type == PanelType.Main || type == PanelType.Room)
                {
                    _presenter.BackMenuButton.Hide();
                }
                else
                {
                    _presenter.BackMenuButton.Show();
                }

                _currentPanel.Hide();
                UIPanel panel = _panels[type];
                _previousPanel = _currentPanel;
                _currentPanel = panel;
                panel.Show();
            }
        }

        public void SwitchPreviousPanel(PanelType type)
        {
            _presenter.BackMenuButton.Hide();
            _currentPanel.Hide();

            if (type != PanelType.Loading)
            {
                _currentPanel = _previousPanel;
            }

            _currentPanel.Show();
        }

        private void OnGameQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void AddListeneres()
        {
            _presenter.MainPanel.FindRoomButton.OnClick += SwitchPanel;
            _presenter.MainPanel.CreateRoomButton.OnClick += SwitchPanel;
            _presenter.MainPanel.SettingsButton.OnClick += SwitchPanel;
            _presenter.MainPanel.QuitGameButton.OnClick += OnGameQuit;
            _presenter.BackMenuButton.OnClick += SwitchPreviousPanel;
        }
    }

    public enum PanelType
    {
        Main,
        Loading,
        FindRoom,
        CreatingRoom,
        Room,
        Settings,
        Back,
        None,
        TeamVsTeam
    }
}
