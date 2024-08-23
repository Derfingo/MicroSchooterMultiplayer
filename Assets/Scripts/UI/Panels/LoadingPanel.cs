using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Panels
{
    public class LoadingPanel : UIPanel
    {
        [SerializeField] private TMP_Text _loadText;
        public TMP_Text LoadText
        {
            get => _loadText;
            set => _loadText = value;
        }
    }
}
