using Assets.Scripts.Save;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Settings
{
    public class SettingsDataProvider : MonoBehaviour
    {
        public event Action<SettingsData> OnSavedData;

        private SettingsData _data;

        [Inject]
        private void Construct(AppSettingsSetter settingsSetter)
        {
            settingsSetter.OnSettingsChanged += OnSaveData;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnSaveData(SettingsData data)
        {
            _data = data;
            OnSavedData?.Invoke(data);
        }

        public SettingsData GetData()
        {
            return _data;
        }
    }
}
