using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.NetworkPhoton
{
    public class PlayerItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNameText;

        private Player _player;

        public string NickName => _player.NickName;

        public void SetPlayerInfo(Player player)
        {
            _player = player;
            _playerNameText.SetText(player.NickName);
        }
    }
}
