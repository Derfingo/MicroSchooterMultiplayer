using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Initialization
{
    public class NetworkInitializer : MonoBehaviourPunCallbacks
    {
        private readonly string _prefabPath = Path.Combine("Prefabs/PhotonPrefabs", "Player Factory");

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == 1)
            {
                PhotonNetwork.Instantiate(_prefabPath, Vector3.zero, Quaternion.identity);
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
