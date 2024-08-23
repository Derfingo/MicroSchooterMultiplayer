using Assets.Scripts.Core.Scenes;
using Assets.Scripts.UI.Presenters;
using Assets.Scripts.UI.Transition;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.NetworkPhoton
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] private LogFeedback _logFeedback;
        private INetworkPresenter _presenter;
        private PanelTransition _panelTransition;
        private SceneTransition _sceneTransition;
        private PlayerList _playerList;
        private RoomList _roomList;

        private Player _masterPlayer;

        [Inject]
        private void Construct(INetworkPresenter presenter, PanelTransition panelTransition, RoomList roomList, PlayerList playerList, SceneTransition sceneTransition)
        {
            _presenter = presenter;
            _panelTransition = panelTransition;
            _sceneTransition = sceneTransition;
            _roomList = roomList;
            _playerList = playerList;

            _presenter.OnEnterPlayerName += EnterPlayerName;
            _presenter.OnCreateRoom += CreateRoom;
            _presenter.OnEnterRoom += JoinRoom;
            _presenter.OnLeaveRoom += LeaveRoom;
            _presenter.OnStartGame += StartGame;

            _logFeedback.AddMessege("Connecting");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            _logFeedback.AddMessege("Connected to Master");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            _logFeedback.AddMessege("Joined Lobby");
        }

        private void EnterPlayerName(string playerName)
        {
            PhotonNetwork.NickName = playerName;
        }

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(_presenter.GetNameRoom))
            {
                return;
            }

            RoomOptions roomOptions = new();
            roomOptions.EmptyRoomTtl = 0;
            PhotonNetwork.CreateRoom(_presenter.GetNameRoom, roomOptions);
            _presenter.SetNameRoom(_presenter.GetNameRoom);
            _panelTransition.SwitchPanel(PanelType.Loading);
        }

        public override void OnJoinedRoom()
        {
            Player[] players = PhotonNetwork.PlayerList;
            _masterPlayer = players[0];
            PhotonNetwork.SetMasterClient(_masterPlayer);
            _playerList.UpdatePlayersList(players);
            _logFeedback.AddMessege($"Master in room: {_masterPlayer.NickName}");
            _presenter.SetStartButtonForMasterClient(PhotonNetwork.IsMasterClient);
            _panelTransition.SwitchPanel(PanelType.Room);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _logFeedback.AddMessege(message);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            _panelTransition.SwitchPanel(PanelType.Loading);
        }

        public override void OnLeftRoom()
        {
            _panelTransition.SwitchPanel(PanelType.Main);
            _roomList.TargetRoomItem = null;
        }

        public void JoinRoom()
        {
            RoomItem roomItem = _roomList.TargetRoomItem;

            if (roomItem != null)
            {
                PhotonNetwork.JoinRoom(roomItem.Info.Name);
                _panelTransition.SwitchPanel(PanelType.Loading);
                _presenter.SetNameRoom(roomItem.Info.Name);
            }
            else
            {
                return;
            }
        }

        private void StartGame()
        {
            _sceneTransition.SwitchScene(TypeScene.Game);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _roomList.UpdateRoomList(roomList);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            _playerList.AddPlayerItem(newPlayer);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            _presenter.SetStartButtonForMasterClient(PhotonNetwork.IsMasterClient);
            _logFeedback.AddMessege($"Master in room: {PhotonNetwork.MasterClient.NickName}");
        }
    }
}
