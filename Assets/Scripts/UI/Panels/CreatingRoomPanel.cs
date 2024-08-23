using Assets.Scripts.UI.Buttons;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class CreatingRoomPanel : UIPanel
    {
        [SerializeField] private TMP_InputField _inputRoomName;
        [SerializeField] private SimpleButton _createRoomButton;

        public TMP_InputField RoomNameText => _inputRoomName;
        public SimpleButton CreateRoomButton
        {
            get => _createRoomButton;
            set => _createRoomButton = value;
        }
    }
}
