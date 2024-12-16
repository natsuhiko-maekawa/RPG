using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class PlayerAilmentStatus : MonoBehaviour
    {
        private PlayerStatusText _playerStatusText;
        private PlayerStatusIcon _playerStatusIcon;

        private void Awake()
        {
            _playerStatusText = GetComponentInChildren<PlayerStatusText>();
            _playerStatusIcon = GetComponentInChildren<PlayerStatusIcon>();
        }

        public void SetActive(bool value)
        {
            _playerStatusIcon.enabled = value;
            if (value)
            {
                _playerStatusText.Activate();
            }
            else
            {
                _playerStatusText.Inactivate();
            }
        }
    }
}