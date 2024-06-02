using System.Collections.Generic;
using System.Linq;
using BattleScene.InterfaceAdapter.Presenter.BuffView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class PlayerBuffView : MonoBehaviour
    {
        [SerializeField] private Sprite buff;
        [SerializeField] private Sprite debuff;
        private List<PlayerStatusView.TextAndIcon> _textAndIconList;

        public void Initialize(List<PlayerStatusView.TextAndIcon> textAndIconList)
        {
            _textAndIconList = textAndIconList;
            foreach (var textAndIcon in _textAndIconList)
            {
                textAndIcon.text.color = Color.gray;
                textAndIcon.icon.enabled = false;
            }
        }

        public void StartAnimation(IList<BuffViewDto> dtoList)
        {
            foreach (var (textAndIcon, index) in _textAndIconList.Select((x, i) => (x, i)))
                switch (dtoList[index].State)
                {
                    case > 0:
                        textAndIcon.text.color = Color.white;
                        textAndIcon.icon.sprite = buff;
                        textAndIcon.icon.enabled = true;
                        break;
                    case 0:
                        textAndIcon.text.color = Color.gray;
                        textAndIcon.icon.enabled = false;
                        break;
                    case < 0:
                        textAndIcon.text.color = Color.white;
                        textAndIcon.icon.sprite = debuff;
                        textAndIcon.icon.enabled = true;
                        break;
                }
        }
    }
}