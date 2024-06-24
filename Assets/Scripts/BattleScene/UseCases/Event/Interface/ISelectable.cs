using UnityEngine;

namespace BattleScene.UseCases.Event.Interface
{
    internal interface ISelectable
    {
        public void SelectAction(Vector2 direction);
    }
}