using System;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.IController
{
    public interface IController
    {
        public void Subscribe(StateMachine stateMachine);
    }
}