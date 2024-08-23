using Assets.Scripts.UI.Buttons;
using Assets.Scripts.UI.Tabs;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class SettingsPanel : UIPanel
    {
        [SerializeField] private GraphicsTab _graphicsTab;
        [SerializeField] private ControlTab _controlTab;
        [SerializeField] private SoundTab _soundTab;
        [SerializeField] private SimpleButton _applySettingsButton;
        [SerializeField] private SimpleButton _defaultSettingButton;

        public GraphicsTab GraphicsTab => _graphicsTab;
        public SimpleButton ApplySettingsButton
        {
            get => _applySettingsButton;
            set => _applySettingsButton = value;
        }

        public SimpleButton DefaultSettingButton
        {
            get => _defaultSettingButton;
            set => _defaultSettingButton = value;
        }

        public ControlTab ControlTab => _controlTab;
    }
}
