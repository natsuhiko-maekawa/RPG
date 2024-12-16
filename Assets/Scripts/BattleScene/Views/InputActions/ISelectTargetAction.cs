using System.Collections.Generic;
using BattleScene.Views.ViewModels;

namespace BattleScene.Views.InputActions
{
    public interface ISelectTargetAction
    {
        public void OnSelect(IReadOnlyList<CharacterModel> targetDtoList);
    }
}