using Assets.Scripts.UI.Buttons;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class RoomPanel : UIPanel
    {
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private SimpleButton _leaveRoomButton;
        [SerializeField] private SimpleButton _startGameButton;

        public TMP_Text RoomNameText
        {
            get => _roomNameText;
            set => _roomNameText = value;
        }

        public SimpleButton LeaveRoomButton
        {
            get => _leaveRoomButton;
            set => _leaveRoomButton = value;
        }

        public SimpleButton StartGameButton
        {
            get => _startGameButton;
            set => _startGameButton = value;
        }
    }
}
