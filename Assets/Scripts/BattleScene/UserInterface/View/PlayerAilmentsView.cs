using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.AilmentsView;
using UnityEngine;

namespace BattleScene.UserInterface.View
{
    public class PlayerAilmentsView : MonoBehaviour
    {
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

        public Task StartAnimation(IList<PlayerAilmentsViewDto> dtoList)
        {
            foreach (var (textAndIcon, index) in _textAndIconList.Select((x, i) => (x, i)))
            {
                textAndIcon.text.color = dtoList.Any(x => x.AilmentsInt == index) ? Color.white : Color.gray;
                textAndIcon.icon.enabled = dtoList.Any(x => x.AilmentsInt == index);
            }

            return Task.CompletedTask;
        }
    }
}