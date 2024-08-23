using Assets.Scripts.Core.Player;
using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.Save
{
    [Serializable]
    public class SettingsData
    {
        public GraphicsData GraphicsData;
        public ControlData ControlData;
        public PlayerData PlayerData;
    }
}
