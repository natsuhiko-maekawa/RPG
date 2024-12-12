using System.Collections.Generic;
using BattleScene.Framework.ViewModels;

namespace BattleScene.Framework.InputActions
{
    public interface ISelectTargetAction
    {
        public void OnSelect(IReadOnlyList<CharacterModel> targetDtoList);
    }
}