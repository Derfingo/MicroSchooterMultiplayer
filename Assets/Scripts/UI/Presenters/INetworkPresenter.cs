using System;

namespace Assets.Scripts.UI.Presenters
{
    public interface INetworkPresenter
    {
        public event Action<string> OnEnterPlayerName;
        public event Action OnCreateRoom;
        public event Action OnEnterRoom;
        public event Action OnLeaveRoom;

        public event Action OnStartGame;

        string GetNameRoom { get; }
        string GetPlayerName { get; }
        void SetNameRoom(string roomName);
        void SetStartButtonForMasterClient(bool isMasterClient);

        public event Action OnAddPlayerToFirstTeam;
        public event Action OnAddPlayerToSecondTeam;
        public event Action OnRemovePlayerFromFirstTeam;
        public event Action OnRemovePlayerFromSecondTeam;
    }
}
