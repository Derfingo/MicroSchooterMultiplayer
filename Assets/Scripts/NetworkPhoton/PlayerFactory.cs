using Assets.Scripts.Core.Player;
using Assets.Scripts.Settings;
using Assets.Scripts.UI.Presenters;
using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.NetworkPhoton
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [Header("Prefabs")]
        [SerializeField] private GameObject _bodyPrefab;
        [SerializeField] private GamePresenter _gamePresenter;
        [SerializeField] private Camera _cameraLook;

        private PhotonView _photonView;

        private SettingsDataProvider _settingsDataProvider;

        private readonly string _gunslingerPath = Path.Combine("Prefabs/PhotonPrefabs", "Gunslinger");
        private readonly string _bodyPath = Path.Combine("Prefabs/PhotonPrefabs", "Body");

        private List<PlayerController> _players;

        private Vector3 _spawnOffset = new(0, 2, 0);

        private void Awake()
        {
            _settingsDataProvider = FindObjectOfType<SettingsDataProvider>();
        }

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _players = new List<PlayerController>();

            if (_photonView.IsMine)
            {
                Create();
            }
        }

        public void Create()
        {
            GameObject player = PhotonNetwork.Instantiate(_gunslingerPath, SetRandomPosition(), Quaternion.identity);
            PlayerController controller = player.GetComponent<PlayerController>();
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            PhotonView playerPhotonView = player.GetComponent<PhotonView>();

            if (playerPhotonView.IsMine)
            {
                var gameCanvas = Instantiate(_gamePresenter, controller.transform);
                var cameraLook = Instantiate(_cameraLook, controller.CameraHolder);
                var canvas = gameCanvas.GetComponent<Canvas>();
                controller.Initialize(cameraLook.transform, gameCanvas);
                controller.SetData(_settingsDataProvider.GetData());
                canvas.worldCamera = cameraLook;

                health.OnDie += ReSpawn;
                _players.Add(controller);
            }
        }

        private Vector3 SetRandomPosition()
        {
            Vector3 spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;
            Collider[] result = new Collider[0];
            Vector3 halfExtents = new(0.1f, 0.1f, 0.1f);
            if (Physics.OverlapBoxNonAlloc(spawnPosition, halfExtents, result) > 0)
            {
                var position = SetRandomPosition();
                position += _spawnOffset;
                return position;
            }

            return spawnPosition;
        }

        private void ReSpawn(PlayerHealth health)
        {
            PlayerController controller = health.GetComponent<PlayerController>();
            Vector3 position = health.transform.position;
            Quaternion rotation = health.transform.rotation;
            controller.SetPosition(SetRandomPosition());
            controller.ResetValues();
            PhotonNetwork.Instantiate(_bodyPath, position, rotation).GetComponent<Rigidbody>();
        }
    }
}
