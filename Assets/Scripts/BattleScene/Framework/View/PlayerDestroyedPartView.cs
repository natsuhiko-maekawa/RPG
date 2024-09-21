using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.ViewModel;
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

        public Task StartAnimation(IList<BodyPartViewModel> dtoList)
        {
            foreach (var (textAndIcon, index) in _textAndIconList.Select((x, i) => (x, i)))
            {
                textAndIcon.text.color = dtoList[index].DestroyedCount > 0 ? Color.white : Color.gray;
                textAndIcon.text.text = dtoList[index].DestroyedCount > 1
                    ? _destroyedPartsList[index]
                    : _destroyedPartList[index];
                textAndIcon.icon.sprite = dtoList[index].DestroyedCount > 1 ? cross : slash;
                textAndIcon.icon.enabled = dtoList[index].DestroyedCount > 0;
            }

            return Task.CompletedTask;
        }
    }
}