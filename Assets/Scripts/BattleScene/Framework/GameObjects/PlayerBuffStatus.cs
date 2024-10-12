using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class PlayerBuffStatus : MonoBehaviour
    {
        [SerializeField] private Sprite buff;
        [SerializeField] private Sprite debuff;
        private PlayerStatusText _playerStatusText;
        private PlayerStatusIcon _playerStatusIcon;

        private void Awake()
        {
            _playerStatusText = GetComponentInChildren<PlayerStatusText>();
            _playerStatusIcon = GetComponentInChildren<PlayerStatusIcon>();
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

        public void Set(int state)
        {
            switch (state)
            {
                case > 0:
                    _playerStatusText.Activate();
                    _playerStatusIcon.Set(buff);
                    _playerStatusIcon.Activate();
                    break;
                case 0:
                    _playerStatusText.Inactivate();
                    _playerStatusIcon.Inactivate();
                    break;
                case < 0:
                    _playerStatusText.Activate();
                    _playerStatusIcon.Set(debuff);
                    _playerStatusIcon.Activate();
                    break;
            }
        }
    }
}