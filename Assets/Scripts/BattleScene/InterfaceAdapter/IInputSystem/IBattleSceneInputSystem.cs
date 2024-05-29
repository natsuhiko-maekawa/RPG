using System;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.IInputSystem
{
    public interface IBattleSceneInputSystem
    {
        public void SetOnNextAction(Action action);
        public void SetOnCancelAction(Action action);
        public void SetOnSelectAction(Action<Vector2> action);
    }
}