using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.ViewModel;
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
                textAndIcon.Text.color = Color.gray;
                textAndIcon.Icon.enabled = false;
            }
        }

        public void StartAnimation(IList<BuffViewModel> dtoList)
        {
            foreach (var (textAndIcon, index) in _textAndIconList.Select((x, i) => (x, i)))
                switch (dtoList[index].State)
                {
                    case > 0:
                        textAndIcon.Text.color = Color.white;
                        textAndIcon.Icon.sprite = buff;
                        textAndIcon.Icon.enabled = true;
                        break;
                    case 0:
                        textAndIcon.Text.color = Color.gray;
                        textAndIcon.Icon.enabled = false;
                        break;
                    case < 0:
                        textAndIcon.Text.color = Color.white;
                        textAndIcon.Icon.sprite = debuff;
                        textAndIcon.Icon.enabled = true;
                        break;
                }
        }
    }
}