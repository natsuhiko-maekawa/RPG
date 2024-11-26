﻿using System;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Facade;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class BuffOutputState : SkillElementOutputState<BuffValueObject>
    {
        private readonly BuffOutput _output;
        private readonly SkillElementStopState<BuffValueObject> _skillElementStopState;

        public BuffOutputState(
            SkillElementStopState<BuffValueObject> skillElementStopState,
            BuffOutput output)
        {
            _skillElementStopState = skillElementStopState;
            _output = output;
        }

        public override void Start()
        {
            if (Context.BattleEventQueue.Count == 0)
                throw new InvalidOperationException(ExceptionMessage.ContextBattleEventQueueIsEmpty);
            var buff = Context.BattleEventQueue.Dequeue();
            _output.Out(buff);
        }

        public override void Select()
        {
            Context.TransitionTo(_skillElementStopState);
        }
    }
}