using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.NetworkNetcode
{
    public class NetworkConnectionPanel : MonoBehaviour
    {
        public Button ServerButton;
        public Button ClientButton;
        public Button HostButton;

        private void Awake()
        {
            ServerButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
            });

            ClientButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });

            HostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });
        }
    }
}
