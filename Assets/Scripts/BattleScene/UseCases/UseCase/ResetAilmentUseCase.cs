using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class ResetAilmentUseCase
    {
        private readonly AilmentResetService _ailmentReset;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public ResetAilmentUseCase(
            AilmentResetService ailmentReset,
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