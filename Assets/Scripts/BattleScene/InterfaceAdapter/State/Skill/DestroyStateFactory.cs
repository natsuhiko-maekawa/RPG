using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DestroyStateFactory
    {
        private readonly DestroyedPartGeneratorService _destroyedPartGenerator;
        private readonly SkillEndState _skillEndState;

        public DestroyStateFactory(
            DestroyedPartGeneratorService destroyedPartGenerator,
            SkillEndState skillEndState)
        {
            _destroyedPartGenerator = destroyedPartGenerator;
            _skillEndState = skillEndState;
        }

        public DestroyState Create(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyedParameterValueObject> destroyedParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var destroyList = _destroyedPartGenerator.Generate(
                skillCommon: skillCommon,
                destroyedParameterList: destroyedParameterList,
                targetIdList: targetIdList);
            var destroyState = new DestroyState(_skillEndState);
            return destroyState;
        }
    }
}