using Assets.Scripts.Initialization;
using Assets.Scripts.Save;
using Assets.Scripts.UI.Presenters;
using System;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public class AppSettingsSetter
    {
        public event Action<SettingsData> OnSettingsChanged;

        private readonly ISettingsPresenter _settingsPresenter;
        private readonly ISaveSystem _saveSystem;
        private readonly GraphicsSettings _graphicsSettings;
        private readonly ControlSettings _controlSettings;

        public AppSettingsSetter(ISettingsPresenter settingsPresenter, ISaveSystem saveSystem, GraphicsSettings graphicsSettings, ControlSettings controlSettings, EntryPoint entryPoint)
        {
            _settingsPresenter = settingsPresenter;
            _graphicsSettings = graphicsSettings;
            _controlSettings = controlSettings;
            _saveSystem = saveSystem;

            entryPoint.OnInitialized += OnLoadSettings;
            _settingsPresenter.OnDefaultSettings += OnApplyDefaultSetting;
            _settingsPresenter.OnApplySettings += OnApplySetting;
        }

        private void OnLoadSettings()
        {
            var data = _saveSystem.Load();

            if (data == null)
            {
                _graphicsSettings.SetDefaultGraphicsSetting();
                _controlSettings.SetDefaultControlSettings();
            }
            else
            {
                _graphicsSettings.SetGraphicsSetting(data.GraphicsData);
                _controlSettings.SetControlSettings(data.ControlData);
            }

            _settingsPresenter.SetResolutionsView(_graphicsSettings.Resolutions);
            _settingsPresenter.UpdateGraphicsView(_graphicsSettings.GraphicsData);
            _settingsPresenter.UpdateControlView(_controlSettings.ControlData);

            OnSettingsChanged?.Invoke(data);
        }

        private void OnApplySetting(SettingsData data)
        {
            _graphicsSettings.SetGraphicsSetting(data.GraphicsData);
            _controlSettings.SetControlSettings(data.ControlData);

            _saveSystem.Save(data);
            Debug.Log("data is saved");
            OnSettingsChanged.Invoke(data);
        }

        private void OnApplyDefaultSetting()
        {
            _graphicsSettings.SetDefaultGraphicsSetting();
            _controlSettings.SetDefaultControlSettings();
        }
    }
}
