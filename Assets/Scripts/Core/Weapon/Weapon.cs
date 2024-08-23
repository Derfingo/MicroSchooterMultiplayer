using UnityEngine;

namespace Assets.Scripts.Core.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
