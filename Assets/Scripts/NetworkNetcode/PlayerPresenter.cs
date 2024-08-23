using UnityEngine;

namespace Assets.Scripts.NetworkNetcode
{
    public class PlayerPresenter : MonoBehaviour
    {
        public GameMenu GameMenu;
        private InputUIMap _inputUI;

        private bool _isPressed;

        private void Awake()
        {
            _inputUI = new InputUIMap();
        }

        private void OnGameMenu()
        {
            if (GameMenu.IsActive)
            {
                GameMenu.Hide();
            }
            else
            {
                GameMenu.Show();
            }
        }

        private void OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnEnable()
        {
            GameMenu.QuitGameButton.onClick.AddListener(OnQuitGame);
            _inputUI.UI.Menu.performed += contex => OnGameMenu();
            _inputUI.Enable();
        }

        private void OnDisable()
        {
            GameMenu.QuitGameButton.onClick.RemoveAllListeners();
            _inputUI.UI.Menu.performed -= contex => OnGameMenu();
            _inputUI.Disable();
        }
    }
}
