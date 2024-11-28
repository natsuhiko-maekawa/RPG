using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.GameObjects
{
    public class ArrowDown : MonoBehaviour
    {
        private Image _downArrow;

        private void Awake()
        {
            _downArrow = GetComponent<Image>();
            _downArrow.enabled = false;
        }

        private void OnEnable()
        {
            _downArrow.enabled = true;
        }

        private void OnDisable()
        {
            _downArrow.enabled = false;
        }
    }
}