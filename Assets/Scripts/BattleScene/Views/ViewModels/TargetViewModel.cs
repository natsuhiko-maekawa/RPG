using System.Collections.Generic;

namespace BattleScene.Views.ViewModels
{
    public struct TargetViewModel
    {
        public IReadOnlyList<CharacterModel> OptionTargetList { get; }
        public IReadOnlyList<int> SelectedTargetIndexList { get; }

        public TargetViewModel(
            IReadOnlyList<CharacterModel> optionTargetList,
            IReadOnlyList<int> selectedTargetIndexList)
        {
            OptionTargetList = optionTargetList;
            SelectedTargetIndexList = selectedTargetIndexList;
        }
    }
}