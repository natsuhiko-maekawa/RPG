using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.DestroyedPartView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class PlayerDestroyedPartView : MonoBehaviour
    {
        [SerializeField] private Sprite cross;
        [SerializeField] private Sprite slash;

        private readonly List<string> _destroyedPartList = new() { "片腕", "片脚", "腹部" };
        private readonly List<string> _destroyedPartsList = new() { "両腕", "両脚", "腹部" };
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

        public Task StartAnimation(IList<PlayerDestroyedPartViewDto> dtoList)
        {
            foreach (var (textAndIcon, index) in _textAndIconList.Select((x, i) => (x, i)))
            {
                textAndIcon.text.color = dtoList[index].DestroyedPartCount > 0 ? Color.white : Color.gray;
                textAndIcon.text.text = dtoList[index].DestroyedPartCount > 1
                    ? _destroyedPartsList[index]
                    : _destroyedPartList[index];
                textAndIcon.icon.sprite = dtoList[index].DestroyedPartCount > 1 ? cross : slash;
                textAndIcon.icon.enabled = dtoList[index].DestroyedPartCount > 0;
            }

            return Task.CompletedTask;
        }
    }
}