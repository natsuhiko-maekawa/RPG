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

        private void OnEnable()
        {
            _upArrow.enabled = true;
        }

        private void OnDisable()
        {
            _upArrow.enabled = false;
        }
    }
}