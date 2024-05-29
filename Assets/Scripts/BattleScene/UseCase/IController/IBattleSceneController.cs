using System;
using UnityEngine;

namespace BattleScene.UseCase.IController
{
    public interface IBattleSceneController
    {
        public void SetOnNextAction(Action action);
        public void SetOnCancelAction(Action action);
        public void SetOnSelectAction(Action<Vector2> action);
    }
}