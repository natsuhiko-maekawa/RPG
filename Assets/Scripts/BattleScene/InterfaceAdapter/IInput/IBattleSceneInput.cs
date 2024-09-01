using System;

namespace BattleScene.InterfaceAdapter.IInput
{
    public interface IBattleSceneInput
    {
        public void SetSelectAction(Action action);
        public void SetCancelAction(Action action);
    }
}