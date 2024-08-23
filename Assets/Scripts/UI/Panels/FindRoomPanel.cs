using Assets.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Panels
{
    public class FindRoomPanel : UIPanel
    {
        [SerializeField] private ScrollRect _roomListScrollRect;
        [SerializeField] private SimpleButton _enterRoomButton;

        public ScrollRect RoomListScrollRect { get => _roomListScrollRect; set => _roomListScrollRect = value; }
        public SimpleButton EnterRoomButton { get => _enterRoomButton; set => _enterRoomButton = value; }
    }
}
