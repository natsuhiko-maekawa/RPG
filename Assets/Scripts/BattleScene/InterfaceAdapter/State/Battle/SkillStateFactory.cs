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
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        
        public SkillStateFactory(
            IObjectResolver container,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            BuffStateFactory buffStateFactory,
            DamageStateFactory damageStateFactory)
        {
            _container = container;
            _skillFactory = skillFactory;
            _buffStateFactory = buffStateFactory;
            _damageStateFactory = damageStateFactory;
        }

        public SkillState Create(SkillCode skillCode) => new SkillState(
            skillCode: skillCode,
            container: _container, 
            skillFactory: _skillFactory,
            buffStateFactory: _buffStateFactory,
            damageStateFactory: _damageStateFactory);
    }
}