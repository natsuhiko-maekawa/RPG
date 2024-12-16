using BattleScene.Views.GameObjects;
using BattleScene.Views.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.Views
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

        public void StartAilmentAnimation(AilmentViewModel ailment)
        {
            _playerAilmentStatusArray[ailment.AilmentId].SetActive(ailment.Effects);
        }

        public void StartDestroyAnimation(BodyPartViewModel bodyPart)
        {
            _playerBodyPartStatusArray[bodyPart.Index].Set(bodyPart.DestroyedCount);
        }

        public void StartBuffAnimation(BuffViewModel buff)
        {
            _playerBuffStatusArray[buff.BuffId].Set(buff.BuffState);
        }
    }
}