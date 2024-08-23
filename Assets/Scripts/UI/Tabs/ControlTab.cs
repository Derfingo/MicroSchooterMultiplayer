using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tabs
{
    public class ControlTab : UIPanel
    {
        [SerializeField] private Slider _horizontalSensitivitySlider;
        [SerializeField] private Slider _verticalSensitivitySlider;
        [SerializeField] private Slider _aimSensitivitySlider;
        [SerializeField] private TMP_Text _horizontalSensitivityValueText;
        [SerializeField] private TMP_Text _verticalSensitivityValueText;
        [SerializeField] private TMP_Text _aimSensitivityValueText;

        public Slider HorizontalSensitivityValue => _horizontalSensitivitySlider;
        public Slider VerticalSensitivityValue => _verticalSensitivitySlider;
        public Slider AimSensitivityValue => _aimSensitivitySlider;

        private readonly string valueFormat = "0.0";

        public void UpdateSliderTextView()
        {
            _horizontalSensitivityValueText.text = _horizontalSensitivitySlider.value.ToString(valueFormat);
            _verticalSensitivityValueText.text = _verticalSensitivitySlider.value.ToString(valueFormat);
            _aimSensitivityValueText.text = _aimSensitivitySlider.value.ToString(valueFormat);
        }

        private void OnEnable()
        {
            _horizontalSensitivitySlider.onValueChanged.AddListener(UpdateHorizontalSensitivityText);
            _verticalSensitivitySlider.onValueChanged.AddListener(UpdateVerticalSensitivityText);
            _aimSensitivitySlider.onValueChanged.AddListener(UpdateAimSensitivityText);
        }

        private void OnDisable()
        {
            _horizontalSensitivitySlider.onValueChanged.RemoveAllListeners();
            _verticalSensitivitySlider.onValueChanged.RemoveAllListeners();
            _aimSensitivitySlider.onValueChanged.RemoveAllListeners();
        }

        private void UpdateHorizontalSensitivityText(float value)
        {
            _horizontalSensitivityValueText.text = value.ToString(valueFormat);
        }

        private void UpdateVerticalSensitivityText(float value)
        {
            _verticalSensitivityValueText.text = value.ToString(valueFormat);
        }

        private void UpdateAimSensitivityText(float value)
        {
            _aimSensitivityValueText.text = value.ToString(valueFormat);
        }
    }
}
