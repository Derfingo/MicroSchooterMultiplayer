using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.NetworkPhoton
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private TMP_Text _amountPlayersText;
        [SerializeField] private Button _selectRoomButton;

        public event Action<RoomItem> OnSelectRoom;

        private RoomInfo _roomInfo;
        public RoomInfo Info => _roomInfo;

        private const int MAX_PLAYERS = 10;

        private void Start()
        {
            _selectRoomButton.onClick.AddListener(OnRoomClick);
        }

        private void OnRoomClick()
        {
            OnSelectRoom?.Invoke(this);
        }

        public void SetRoomInfo(RoomInfo info)
        {
            _roomInfo = info;
            _roomNameText.SetText(info.Name);
            _amountPlayersText.SetText(info.PlayerCount.ToString() + "/" + MAX_PLAYERS);
        }

        public RoomInfo GetRoomInfo()
        {
            return _roomInfo;
        }
    }
}
