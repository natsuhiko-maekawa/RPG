using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class PlayerAilmentStatus : MonoBehaviour
    {
        private PlayerStatusText _playerStatusText;
        private PlayerStatusIcon _playerStatusIcon;

        private void Awake()
        {
            _playerStatusText = GetComponentInChildren<PlayerStatusText>();
            _playerStatusIcon = GetComponentInChildren<PlayerStatusIcon>();
            Inactivate();
        }

        public void Activate()
        {
            _playerStatusText.Activate();
            _playerStatusIcon.Activate();
        }

        public void Inactivate()
        {
            _playerStatusText.Inactivate();
            _playerStatusIcon.Inactivate();
        }
    }
}