using Assets.Scripts.Save;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public class GraphicsSettings
    {
        private GraphicsData _graphicsData;
        private readonly List<Resolution> _resolutions;
        private int _lastResolutionIndex;
        public GraphicsData GraphicsData => _graphicsData;
        public List<Resolution> Resolutions => _resolutions;

        public GraphicsSettings()
        {
            _graphicsData = new GraphicsData();
            _resolutions = new List<Resolution>();
            DefineResolutions();
        }

        public void SetDefaultGraphicsSetting()
        {
            SetQuality(0);
            SetVsync(false);
            SetFrameRate(60);
            SetFullScreen(true);
            SetResolution(_lastResolutionIndex);
        }

        public void SetGraphicsSetting(GraphicsData data)
        {
            SetQuality(data.QualityIndex);
            SetVsync(data.IsVsync);
            SetFrameRate(data.FrameRateValue);
            SetFullScreen(data.IsFullScreen);
            SetResolution(data.ResolutionIndex);
        }

        private void SetResolution(int resolutionIndex)
        {
#if UNITY_EDITOR

            Debug.Log("resolution isn't set: EDITOR");
            return;

#elif UNITY_STANDALONE

            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            _graphicsData.ResolutionIndex = resolutionIndex;

#endif
        }


        private void SetQuality(int Qualityindex)
        {
            QualitySettings.SetQualityLevel(Qualityindex);
            _graphicsData.QualityIndex = Qualityindex;
        }

        private void SetFrameRate(int frameRateValue)
        {
            Application.targetFrameRate = frameRateValue;
            _graphicsData.FrameRateValue = frameRateValue;
        }

        private void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            _graphicsData.IsFullScreen = isFullScreen;
        }

        private void SetVsync(bool vSync)
        {
            QualitySettings.vSyncCount = vSync ? 1 : 0;
            _graphicsData.IsVsync = vSync;
        }

        private void DefineResolutions()
        {
            var resolutions = Screen.resolutions;
            var currentResolution = Screen.currentResolution;

            foreach (var resolution in resolutions)
            {
                if (resolution.refreshRate == currentResolution.refreshRate)
                {
                    _resolutions.Add(resolution);
                }
            }

            _lastResolutionIndex = _resolutions.IndexOf(currentResolution);
        }
    }

    [Serializable]
    public struct GraphicsData : ISaveStructure
    {
        public int ResolutionIndex;
        public int QualityIndex;
        public int FrameRateValue;
        public bool IsFullScreen;
        public bool IsVsync;
    }
}
