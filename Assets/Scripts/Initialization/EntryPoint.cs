using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Initialization
{
    public class EntryPoint : MonoBehaviour
    {
        public event Action OnInitialized;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            OnInitialized.Invoke();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
