using Assets.Scripts.UI.Buttons;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class MainPanel : UIPanel
    {
        [SerializeField] private RectTransform _roomButtonsMenu;
        [SerializeField] private RectTransform _inputPlayerNameMenu;
        [SerializeField] private TMP_Text _gameNameText;
        [SerializeField] private TMP_InputField _inputPlayerName;
        [SerializeField] private MenuButton _findRoomButton;
        [SerializeField] private MenuButton _createRoomButton;
        [SerializeField] private MenuButton _settingsButton;
        [SerializeField] private SimpleButton _quitGameButton;
        [SerializeField] private SimpleButton _enterPlayerNameButton;

        private async void Start()
        {
            Task task = AnimateTitle();
            await task;
            AnimateButtons();
        }

        private Task AnimateTitle()
        {
            return DOTween.Sequence()
                .Append(_gameNameText.rectTransform.DOAnchorPos3DY(-440f, 0.5f))
                .AppendInterval(0.5f)
                .AsyncWaitForCompletion();
        }

        private void AnimateButtons()
        {
            _gameNameText.rectTransform.DOAnchorPos3DY(-440f, 0.5f);
            _inputPlayerNameMenu.DOAnchorPos3DY(0f, 0.5f);
            _quitGameButton.GetComponent<RectTransform>().DOAnchorPos3DY(50f, 0.5f);
            _settingsButton.GetComponent<RectTransform>().DOAnchorPos3DY(-50f, 0.5f);
        }

        public MenuButton FindRoomButton => _findRoomButton;
        public MenuButton CreateRoomButton => _createRoomButton;
        public MenuButton SettingsButton => _settingsButton;
        public SimpleButton QuitGameButton => _quitGameButton;
        public SimpleButton EnterPlayerNameButton => _enterPlayerNameButton;
        public TMP_InputField InputPlayerName
        {
            get => _inputPlayerName;
            set => _inputPlayerName = value;
        }

        public RectTransform RoomButtonsMenu => _roomButtonsMenu;
        public RectTransform InputPlayerNameMenu => _inputPlayerNameMenu;
        public TMP_Text GameNameText => _gameNameText;
    }
}
