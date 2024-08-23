using Assets.Scripts.Save;
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Presenters
{
    public interface ISettingsPresenter
    {
        public event Action<SettingsData> OnApplySettings;
        public event Action OnDefaultSettings;

        int QualityIndex { get; }
        int ResolutionIndex { get; }
        int FrameRateValue { get; }
        bool IsFullScreen { get; }
        bool IsVsync { get; }

        float HorizontalSensitivityValue { get; }
        float VerticalSensitivityValue { get; }
        float AimSensitivityValue { get; }

        void UpdateGraphicsView(GraphicsData data);
        void UpdateControlView(ControlData data);
        void SetResolutionsView(List<Resolution> resolutions);
    }
}
