﻿using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Skill;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SkillStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly DamageStateFactory _damageStateFactory;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            DamageStateFactory damageStateFactory)
        {
            _container = container;
            _skillFactory = skillFactory;
            _damageStateFactory = damageStateFactory;
        }

        public SkillState Create(SkillCode skillCode) => new SkillState(
            skillCode: skillCode,
            container: _container, 
            skillFactory: _skillFactory,
            damageStateFactory: _damageStateFactory);
    }
}