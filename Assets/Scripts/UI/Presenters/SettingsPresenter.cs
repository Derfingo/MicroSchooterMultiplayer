using Assets.Scripts.Save;
using Assets.Scripts.Settings;
using Assets.Scripts.UI.Panels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Presenters
{
    public class SettingsPresenter : UIElement, ISettingsPresenter
    {
        [SerializeField] private SettingsPanel _settingsPanel;
        public SettingsPanel SettingsPanel => _settingsPanel;

        public event Action<SettingsData> OnApplySettings;
        public event Action OnDefaultSettings;

        public int QualityIndex => _settingsPanel.GraphicsTab.GraphicsQualityDropdown.value;
        public int ResolutionIndex => _settingsPanel.GraphicsTab.ScreenResolutionDropdown.value;
        public int FrameRateValue => (int)_settingsPanel.GraphicsTab.FPSSlider.value;
        public bool IsFullScreen => _settingsPanel.GraphicsTab.FullScreenToggle.isOn;
        public bool IsVsync => _settingsPanel.GraphicsTab.VSyncToggle.isOn;

        public float HorizontalSensitivityValue => _settingsPanel.ControlTab.HorizontalSensitivityValue.value;
        public float VerticalSensitivityValue => _settingsPanel.ControlTab.VerticalSensitivityValue.value;
        public float AimSensitivityValue => _settingsPanel.ControlTab.AimSensitivityValue.value;

        private void Start()
        {
            _settingsPanel.ApplySettingsButton.OnClick += OnAppliedSettings;
            _settingsPanel.DefaultSettingButton.OnClick += OnDefaultSetSettring;
            DontDestroyOnLoad(this);
        }

        public void UpdateGraphicsView(GraphicsData data)
        {
            _settingsPanel.GraphicsTab.GraphicsQualityDropdown.value = data.QualityIndex;
            _settingsPanel.GraphicsTab.ScreenResolutionDropdown.value = data.ResolutionIndex;
            _settingsPanel.GraphicsTab.FPSSlider.value = data.FrameRateValue;
            _settingsPanel.GraphicsTab.FullScreenToggle.isOn = data.IsFullScreen;
            _settingsPanel.GraphicsTab.VSyncToggle.isOn = data.IsVsync;
            _settingsPanel.GraphicsTab.UpdateSliderTextView();
        }

        public void UpdateControlView(ControlData data)
        {
            _settingsPanel.ControlTab.HorizontalSensitivityValue.value = data.HorizontalSensitivity;
            _settingsPanel.ControlTab.VerticalSensitivityValue.value = data.VerticalSensitivity;
            _settingsPanel.ControlTab.AimSensitivityValue.value = data.AimSensitivity;
            _settingsPanel.ControlTab.UpdateSliderTextView();
        }

        public void SetResolutionsView(List<Resolution> resolutions)
        {
            _settingsPanel.GraphicsTab.ScreenResolutionDropdown.ClearOptions();
            List<string> resolutionsText = new();

            foreach (var resolution in resolutions)
            {
                string option = resolution.width + " x " + resolution.height;
                resolutionsText.Add(option);
            }

            _settingsPanel.GraphicsTab.ScreenResolutionDropdown.AddOptions(resolutionsText);
        }

        private void OnAppliedSettings()
        {
            SettingsData data = new()
            {
                GraphicsData = new GraphicsData
                {
                    QualityIndex = QualityIndex,
                    ResolutionIndex = ResolutionIndex,
                    FrameRateValue = FrameRateValue,
                    IsFullScreen = IsFullScreen,
                    IsVsync = IsVsync
                },

                ControlData = new ControlData
                {
                    HorizontalSensitivity = HorizontalSensitivityValue,
                    VerticalSensitivity = VerticalSensitivityValue,
                    AimSensitivity = AimSensitivityValue
                }

            };

            OnApplySettings.Invoke(data);
        }

        private void OnDefaultSetSettring()
        {
            OnDefaultSettings.Invoke();
        }
    }
}
