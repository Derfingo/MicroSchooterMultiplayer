using Assets.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class GameMenuPanel : UIPanel
    {
        [SerializeField] private SimpleButton _quitGameButton;
        [SerializeField] private SimpleButton _settingsMenuButton;
        [SerializeField] private SimpleButton _backMainMenuButton;

        public SimpleButton QuitGameButton => _quitGameButton;
        public SimpleButton SettingsMenuButton => _settingsMenuButton;
        public SimpleButton BackMainMenuButton => _backMainMenuButton;
    }
}
