using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class PlayerStatusText : MonoBehaviour
    {
        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color inactiveColor = Color.gray;
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void Set(string text)
        {
            _text.text = text;
        }

        public void Activate()
        {
            _text.color = activeColor;
        }

        public void Inactivate()
        {
            _text.color = inactiveColor;
        }
    }
}