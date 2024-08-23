using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.NetworkPhoton
{
    public class PlayerList : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerItem _playerItemPrefab;
        [SerializeField] private Transform _playerListContent;
        [SerializeField] private LogFeedback _logFeedback;

        private readonly Dictionary<string, PlayerItem> _playerItems = new();

        public void AddPlayerItem(Player newPlayer)
        {
            if (_playerItems.ContainsKey(newPlayer.NickName))
            {
                _logFeedback.AddMessege("This name already exists");
                return;
            }
            else
            {
                PlayerItem playerItem = Instantiate(_playerItemPrefab, _playerListContent);
                playerItem.SetPlayerInfo(newPlayer);
                _playerItems.Add(playerItem.NickName, playerItem);
            }
        }

        public void UpdatePlayersList(Player[] players)
        {
            ClearPlayerList();

            for (int i = 0; i < players.Count(); i++)
            {
                var playerItem = Instantiate(_playerItemPrefab, _playerListContent);
                playerItem.SetPlayerInfo(players[i]);
                _playerItems.Add(players[i].NickName, playerItem);
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (_playerItems.ContainsKey(otherPlayer.NickName))
            {
                Destroy(_playerItems[otherPlayer.NickName].gameObject);
                _playerItems.Remove(otherPlayer.NickName);
            }
        }

        public void ClearPlayerList()
        {
            foreach (Transform transform in _playerListContent)
            {
                Destroy(transform.gameObject);
            }

            _playerItems.Clear();
        }

        public override void OnLeftRoom()
        {
            ClearPlayerList();
        }
    }
}
