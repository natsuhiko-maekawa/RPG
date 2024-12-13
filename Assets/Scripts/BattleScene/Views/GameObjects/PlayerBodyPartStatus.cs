using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class PlayerBodyPartStatus : MonoBehaviour
    {
        [SerializeField] private Sprite cross;
        [SerializeField] private Sprite slash;
        [SerializeField] private string singleName;
        [SerializeField] private string multipleName;
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

        public void Set(int destroyedCount)
        {
            switch (destroyedCount)
            {
                case 0:
                    NoDestroy();
                    break;
                case 1:
                    DestroyedSingle();
                    break;
                default:
                    DestroyedMultiple();
                    break;
            }
        }

        private void NoDestroy()
        {
            _playerStatusText.Set(singleName);
            Inactivate();
        }

        private void DestroyedSingle()
        {
            _playerStatusText.Set(singleName);
            _playerStatusIcon.Set(slash);
            Activate();
        }

        private void DestroyedMultiple()
        {
            _playerStatusText.Set(multipleName);
            _playerStatusIcon.Set(cross);
            Activate();
        }
    }
}