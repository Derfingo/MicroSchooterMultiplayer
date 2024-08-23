using Photon.Pun;

namespace Assets.Scripts.Core.Scenes
{
    public class SceneTransition
    {
        public void SwitchScene(TypeScene type)
        {
            PhotonNetwork.LoadLevel((int)type);
        }
    }

    public enum TypeScene
    {
        Main,
        Game
    }
}
