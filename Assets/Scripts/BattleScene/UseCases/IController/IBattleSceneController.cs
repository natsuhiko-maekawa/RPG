using System;
using UnityEngine;

namespace BattleScene.UseCases.IController
{
    public interface IBattleSceneController
    {
        public void SetOnNextAction(Action action);
        public void SetOnCancelAction(Action action);
        public void SetOnSelectAction(Action<Vector2> action);
    }
}