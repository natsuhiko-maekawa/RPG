using System;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.UseCase
{
    public class ResetAilmentUseCase
    {
        private readonly IAilmentResetService _ailmentReset;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public ResetAilmentUseCase(
            IAilmentResetService ailmentReset,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _ailmentReset = ailmentReset;
            _skillFactory = skillFactory;
        }

        public void Reset(AilmentCode ailmentCode)
        {
            if (ailmentCode == AilmentCode.NoAilment)
                throw new ArgumentException(ExceptionMessage.AilmentCodeIsDefaultValue);
            _ailmentReset.Reset(ailmentCode);
        }

        public SkillValueObject GetAttackSkill()
        {
            var skill = _skillFactory.Create(SkillCode.Attack);
            return skill;
        }
    }
}