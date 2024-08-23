using Assets.Scripts.Save;
using System;

namespace Assets.Scripts.Settings
{
    public class ControlSettings
    {
        private ControlData _config;
        public ControlData ControlData => _config;

        public ControlSettings()
        {
            _config = new ControlData();
        }

        private void SetSensitivity(float horizontal, float vertical, float aim)
        {
            _config.HorizontalSensitivity = horizontal;
            _config.VerticalSensitivity = vertical;
            _config.AimSensitivity = aim;
        }

        public void SetDefaultControlSettings()
        {
            _config.HorizontalSensitivity = 0.5f;
            _config.VerticalSensitivity = 0.5f;
            _config.AimSensitivity = 0.5f;
        }

        public void SetControlSettings(ControlData data)
        {
            SetSensitivity(data.HorizontalSensitivity, data.VerticalSensitivity, data.AimSensitivity);
        }
    }

    [Serializable]
    public struct ControlData : ISaveStructure
    {
        public float HorizontalSensitivity;
        public float VerticalSensitivity;
        public float AimSensitivity;
    }
}
