using System.Collections.Generic;
using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class PlayerStatusView : MonoBehaviour, IPlayerStatusView
    {
        private Image[] _iconGroup;
        private PlayerAilmentsView _playerAilmentsView;
        private PlayerBuffView _playerBuffView;
        private PlayerDestroyedPartView _playerDestroyedPartView;
        private Text[] _textGroup;

        private void Awake()
        {
            var groups = GetComponentsInChildren<GridLayoutGroup>();
            _textGroup = groups[0].GetComponentsInChildren<Text>();
            _iconGroup = groups[1].GetComponentsInChildren<Image>();
            _playerAilmentsView = GetComponent<PlayerAilmentsView>();
            _playerDestroyedPartView = GetComponent<PlayerDestroyedPartView>();
            _playerBuffView = GetComponent<PlayerBuffView>();

            foreach (var icon in _iconGroup) icon.enabled = false;

            var ailmentsViewTextAndIconList = Enumerable.Range(1, 15)
                .Select(x => new TextAndIcon
                {
                    text = _textGroup[x],
                    icon = _iconGroup[x]
                })
                .ToList();
            _playerAilmentsView.Initialize(ailmentsViewTextAndIconList);

            var destroyedPartViewTextAndIconList = Enumerable.Range(17, 3)
                .Select(x => new TextAndIcon
                {
                    text = _textGroup[x],
                    icon = _iconGroup[x]
                })
                .ToList();
            _playerDestroyedPartView.Initialize(destroyedPartViewTextAndIconList);

            var indexList = new List<int> { 21, 22, 24, 25, 26 };
            var buffViewTextAndIconList = indexList
                .Select(x => new TextAndIcon
                {
                    text = _textGroup[x],
                    icon = _iconGroup[x]
                })
                .ToList();
            _playerBuffView.Initialize(buffViewTextAndIconList);
        }

        public void StartPlayerAilmentsView(IList<PlayerAilmentsViewDto> dtoList)
        {
            _playerAilmentsView.StartAnimation(dtoList);
        }

        public void StartPlayerDestroyedPartView(IList<PlayerDestroyedPartViewDto> dtoList)
        {
            _playerDestroyedPartView.StartAnimation(dtoList);
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