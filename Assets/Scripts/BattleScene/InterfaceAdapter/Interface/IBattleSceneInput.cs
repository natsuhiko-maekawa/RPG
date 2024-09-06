using System;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IBattleSceneInput
    {
        public void SetSelectAction(Action action);
        public void SetCancelAction(Action action);
    }
}