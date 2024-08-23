using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tabs
{
    public class GraphicsTab : UIPanel
    {
        [SerializeField] private TMP_Dropdown _screenResolutionDropdown;
        [SerializeField] private TMP_Dropdown _graphicsQualityDropdown;
        [SerializeField] private Slider _fpsSlider;
        [SerializeField] private TMP_Text _fpsValueText;
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Toggle _vSyncToggle;

        public TMP_Dropdown ScreenResolutionDropdown => _screenResolutionDropdown;
        public TMP_Dropdown GraphicsQualityDropdown => _graphicsQualityDropdown;
        public Slider FPSSlider => _fpsSlider;
        public Toggle FullScreenToggle => _fullScreenToggle;
        public Toggle VSyncToggle => _vSyncToggle;

        public void UpdateSliderTextView()
        {
            _fpsValueText.text = _fpsSlider.value.ToString();
        }

        private void OnEnable()
        {
            _fpsSlider.onValueChanged.AddListener(UpdateFPSText);
        }

        private void OnDisable()
        {
            _fpsSlider.onValueChanged.RemoveAllListeners();
        }

        private void UpdateFPSText(float value)
        {
            _fpsValueText.text = value.ToString();
        }
    }
}
