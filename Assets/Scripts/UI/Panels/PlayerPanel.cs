using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Panels
{
    public class PlayerPanel : UIPanel
    {
        [SerializeField] private Image _healthbarImage;
        [SerializeField] private TMP_Text _healthbarText;

        public Image HealthbarImage
        {
            get => _healthbarImage;
            set => _healthbarImage = value;
        }

        public TMP_Text HealthbarText
        {
            get => _healthbarText;
            set => _healthbarText = value;
        }
    }
}
