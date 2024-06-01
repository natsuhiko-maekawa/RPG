using UnityEngine;

namespace BattleScene.UseCase.Event.Interface
{
    internal interface ISelectable
    {
        public void SelectAction(Vector2 direction);
    }
}