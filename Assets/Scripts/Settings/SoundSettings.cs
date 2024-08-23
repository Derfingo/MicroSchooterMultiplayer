using Assets.Scripts.Save;
using System;
using UnityEngine.Audio;

namespace Assets.Scripts.Settings
{
    public class SoundSettings
    {
        private AudioMixer _audioMixer;
        private SoundData _soundData;
        public SoundData SoundData => _soundData;

        public SoundSettings()
        {
            _soundData = new SoundData();
        }

        private void SetVolume(float volume)
        {
            _audioMixer.SetFloat("volume", volume);
            _soundData.Volume = volume;
        }
    }

    [Serializable]
    public struct SoundData : ISaveStructure
    {
        public float Volume;
    }
}
