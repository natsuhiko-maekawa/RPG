using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class PlayerStatusView : MonoBehaviour
    {
        private Image[] _iconGroup;
        private PlayerAilmentStatus[] _playerAilmentStatusArray;
        private PlayerBodyPartStatus[] _playerBodyPartStatusArray;
        private PlayerBuffStatus[] _playerBuffStatusArray;
        private Text[] _textGroup;

        private void Awake()
        {
            _playerAilmentStatusArray = GetComponentsInChildren<PlayerAilmentStatus>();
            _playerBodyPartStatusArray = GetComponentsInChildren<PlayerBodyPartStatus>();
            _playerBuffStatusArray = GetComponentsInChildren<PlayerBuffStatus>();
        }

        public void StartPlayerAilmentsView(AilmentViewModel ailment)
        {
            if (ailment.Effects) _playerAilmentStatusArray[ailment.AilmentId].Activate();
            else _playerAilmentStatusArray[ailment.AilmentId].Inactivate();
        }

        public void StartPlayerDestroyedPartView(BodyPartViewModel bodyPart)
        {
            _playerBodyPartStatusArray[bodyPart.Index].Set(bodyPart.DestroyedCount);
        }

        public void StartPlayerBuffView(BuffViewModel buff)
        {
            _playerBuffStatusArray[buff.BuffId].Set(buff.State);
        }
    }
}