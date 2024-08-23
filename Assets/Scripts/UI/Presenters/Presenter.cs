using Assets.Scripts.UI.Buttons;
using Assets.Scripts.UI.Panels;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Presenters
{
    public class Presenter : MonoBehaviour, IMenuPresenter, INetworkPresenter
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private CreatingRoomPanel _creatingRoomPanel;
        [SerializeField] private FindRoomPanel _findRoomPanel;
        [SerializeField] private RoomPanel _roomPanel;
        [SerializeField] private MenuButton _backMenuButton;
        [SerializeField] private TeamVsTeamPanel _teamVsTeamPanel;

        public event Action OnStartGame;

        public event Action<string> OnEnterPlayerName;
        public event Action OnCreateRoom;
        public event Action OnEnterRoom;
        public event Action OnLeaveRoom;

        public event Action OnAddPlayerToFirstTeam;
        public event Action OnAddPlayerToSecondTeam;
        public event Action OnRemovePlayerFromFirstTeam;
        public event Action OnRemovePlayerFromSecondTeam;

        public string GetNameRoom => _creatingRoomPanel.RoomNameText.text;
        public string GetPlayerName => _mainPanel.InputPlayerName.text;
        public MainPanel MainPanel => _mainPanel;
        public MenuButton BackMenuButton => _backMenuButton;

        [Inject]
        private void Construct()
        {
            _mainPanel.EnterPlayerNameButton.OnClick += EnterPlayerName;
            _creatingRoomPanel.CreateRoomButton.OnClick += CreateRoom;
            _findRoomPanel.EnterRoomButton.OnClick += EnterRoom;
            _roomPanel.StartGameButton.OnClick += StartGame;
            _roomPanel.LeaveRoomButton.OnClick += LeaveRoom;
            _teamVsTeamPanel.EnterFirstTeamButton.OnClick += AddPlayerToFirstTeam;
            _teamVsTeamPanel.EnterSecondTeamButton.OnClick += AddPlayerToSecondTeam;
        }

        private void EnterPlayerName()
        {
            OnEnterPlayerName?.Invoke(_mainPanel.InputPlayerName.text);
            _mainPanel.InputPlayerNameMenu.gameObject.SetActive(false);
            _mainPanel.RoomButtonsMenu.gameObject.SetActive(true);
        }

        private void CreateRoom()
        {
            OnCreateRoom?.Invoke();
        }

        private void EnterRoom()
        {
            OnEnterRoom?.Invoke();
        }

        private void LeaveRoom()
        {
            OnLeaveRoom?.Invoke();
        }

        private void StartGame()
        {
            OnStartGame?.Invoke();
        }

        private void AddPlayerToFirstTeam()
        {
            OnAddPlayerToFirstTeam?.Invoke();
        }
        private void AddPlayerToSecondTeam()
        {
            OnAddPlayerToSecondTeam?.Invoke();
        }

        public void SetNameRoom(string roomName)
        {
            _roomPanel.RoomNameText.SetText(roomName);
        }

        public void SetStartButtonForMasterClient(bool isMasterClient)
        {
            _roomPanel.StartGameButton.gameObject.SetActive(isMasterClient);
        }
    }
}
