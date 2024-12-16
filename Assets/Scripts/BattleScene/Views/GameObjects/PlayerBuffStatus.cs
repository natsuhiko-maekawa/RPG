using System;
using BattleScene.Views.ViewModels;
using UnityEngine;

namespace BattleScene.Views.GameObjects
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
                    _playerStatusIcon.enabled = true;
                    break;
                case BuffState.NoBuff:
                    _playerStatusText.Inactivate();
                    _playerStatusIcon.enabled = false;
                    break;
                case BuffState.DeBuff:
                    _playerStatusText.Activate();
                    _playerStatusIcon.Set(debuff);
                    _playerStatusIcon.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buffState), buffState, null);
            }
        }
    }
}