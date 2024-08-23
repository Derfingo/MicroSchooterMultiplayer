using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;

namespace Assets.Scripts.NetworkPhoton
{
    public class TeamList : MonoBehaviourPunCallbacks
    {
        public event Action<Player, PhotonTeam> OnAddPlayerToTeam = delegate { };
        public event Action<Player> OnRemovePlayerFromTeam = delegate { };

        private PhotonTeam _photonTeam;
        private PhotonTeamsManager _photonTeamManager;

        private void CreateTeams()
        {

        }

        private void SwitchTeam()
        {

        }
    }
}
