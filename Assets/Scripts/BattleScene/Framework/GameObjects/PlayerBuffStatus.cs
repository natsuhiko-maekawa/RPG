using System;
using BattleScene.Framework.ViewModel;
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

        public void Set(BuffState buffState)
        {
            switch (buffState)
            {
                case BuffState.Buff:
                    _playerStatusText.Activate();
                    _playerStatusIcon.Set(buff);
                    _playerStatusIcon.Activate();
                    break;
                case BuffState.NoBuff:
                    _playerStatusText.Inactivate();
                    _playerStatusIcon.Inactivate();
                    break;
                case BuffState.DeBuff:
                    _playerStatusText.Activate();
                    _playerStatusIcon.Set(debuff);
                    _playerStatusIcon.Activate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buffState), buffState, null);
            }
        }
    }
}