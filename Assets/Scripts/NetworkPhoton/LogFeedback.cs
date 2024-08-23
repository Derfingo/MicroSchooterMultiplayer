using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.NetworkPhoton
{
    public class LogFeedback : MonoBehaviour
    {
        [SerializeField] private TMP_Text _feedbackText;
        private readonly StringBuilder _text = new();

        public void AddMessege(string message)
        {
            _text.Append(message + " ->" + System.Environment.NewLine);
            _feedbackText.text = _text.ToString();
        }
    }
}
