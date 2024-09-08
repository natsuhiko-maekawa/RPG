using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class ArrowUp : MonoBehaviour
    {
        private Image _upArrow;
        
        private void Awake()
        {
            _upArrow = GetComponent<Image>();
            _upArrow.enabled = false;
        }

        public void Show(bool value) => _upArrow.enabled = value;
    }
}