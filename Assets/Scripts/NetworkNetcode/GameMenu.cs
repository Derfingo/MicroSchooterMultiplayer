using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.NetworkNetcode
{
    public class GameMenu : MonoBehaviour
    {
        public Button QuitGameButton;

        public bool IsActive { get; private set; }

        public void Hide()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void Show()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }
    }
}
