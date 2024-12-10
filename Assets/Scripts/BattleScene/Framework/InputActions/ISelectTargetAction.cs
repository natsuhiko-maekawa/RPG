using System.Collections.Generic;
using BattleScene.Framework.ViewModel;

namespace BattleScene.Framework.InputActions
{
    public interface ISelectTargetAction
    {
        public void OnSelect(IReadOnlyList<Character> targetDtoList);
    }
}