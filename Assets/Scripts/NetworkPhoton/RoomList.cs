using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.NetworkPhoton
{
    public class RoomList : MonoBehaviour
    {
        [SerializeField] private RoomItem _roomItemPrefab;
        [SerializeField] private Transform _roomListContent;

        private readonly Dictionary<string, RoomInfo> _roomList = new();
        private readonly List<RoomItem> _roomItems = new();
        private RoomItem _targetRoomItem;

        public RoomItem TargetRoomItem
        {
            get => _targetRoomItem;
            set => _targetRoomItem = value;
        }

        private void ClearRoomList()
        {
            foreach (Transform transform in _roomListContent)
            {
                Destroy(transform.gameObject);
            }

            _roomItems.Clear();
            _roomList.Clear();
        }

        private void FillRoomList(List<RoomInfo> roomList)
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].RemovedFromList)
                {
                    continue;
                }

                var roomItem = Instantiate(_roomItemPrefab, _roomListContent);
                roomItem.SetRoomInfo(roomList[i]);
                _roomItems.Add(roomItem);
                roomItem.OnSelectRoom += SelectRoom;
            }
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            ClearRoomList();
            FillRoomList(roomList);
        }

        private void SelectRoom(RoomItem roomItem)
        {
            _targetRoomItem = roomItem;
        }
    }
}
