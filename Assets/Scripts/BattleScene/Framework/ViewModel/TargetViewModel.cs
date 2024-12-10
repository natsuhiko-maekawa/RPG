using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public struct TargetViewModel
    {
        public IReadOnlyList<Character> OptionTargetList { get; }
        public IReadOnlyList<int> SelectedTargetIndexList { get; }

        public TargetViewModel(
            IReadOnlyList<Character> optionTargetList,
            IReadOnlyList<int> selectedTargetIndexList)
        {
            OptionTargetList = optionTargetList;
            SelectedTargetIndexList = selectedTargetIndexList;
        }
    }
}