using System;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.IInputSystem
{
    public interface IBattleSceneInput
    {
        public void SetSelectAction(Action action);
        public void SetCancelAction(Action action);
    }
}