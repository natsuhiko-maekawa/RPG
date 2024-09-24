using System.Collections.Generic;
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
        private PlayerBuffView _playerBuffView;
        private Text[] _textGroup;

        private void Awake()
        {
            _playerAilmentStatusArray = GetComponentsInChildren<PlayerAilmentStatus>();
            _playerBodyPartStatusArray = GetComponentsInChildren<PlayerBodyPartStatus>();
            _playerBuffView = GetComponent<PlayerBuffView>();
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

        public void StartPlayerBuffView(IList<BuffViewDto> dtoList)
        {
            _playerBuffView.StartAnimation(dtoList);
        }

        public struct TextAndIcon
        {
            public Text text;
            public Image icon;
        }
    }
}