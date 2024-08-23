using Assets.Scripts.UI.Panels;
using UnityEngine;

namespace Assets.Scripts.UI.Presenters
{
    public class GamePresenter : UIElement, IGamePresenter
    {
        [SerializeField] private GameMenuPanel _gameMenuPanel;
        [SerializeField] private PlayerPanel _playerPanel;

        private SettingsPresenter _presenter;
        private SettingsPanel _settingsPanel;

        private InputUIMap _inputUI;

        private void Awake()
        {
            _presenter = FindObjectOfType<SettingsPresenter>();
            _settingsPanel = _presenter.SettingsPanel;
        }

        private void Start()
        {
            _inputUI = new InputUIMap();
            _inputUI.Enable();
            _inputUI.UI.Menu.performed += contex => OnGameMenu();
            _gameMenuPanel.QuitGameButton.OnClick += QuitGame;
            _gameMenuPanel.SettingsMenuButton.OnClick += OnSettingsMenu;
            _gameMenuPanel.BackMainMenuButton.OnClick += OnMainMenu;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnGameMenu()
        {
            if (_gameMenuPanel.isActiveAndEnabled)
            {
                _gameMenuPanel.Hide();
                _settingsPanel.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                _gameMenuPanel.Show();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void OnHealthbarUpdate(float healthValue)
        {
            _playerPanel.HealthbarImage.fillAmount = healthValue / 100;
            _playerPanel.HealthbarText.text = Mathf.FloorToInt(healthValue).ToString();
        }

        private void OnSettingsMenu()
        {
            _settingsPanel.gameObject.SetActive(true);
        }

        private void OnMainMenu()
        {
            // back to maim menu
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
