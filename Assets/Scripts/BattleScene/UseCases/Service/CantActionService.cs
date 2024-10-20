using System;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.Service
{
    public class CantActionService
    {
        public SkillCode ToSkillCode(AilmentCode ailmentCode)
        {
            var skillCode = ailmentCode switch
            {
                AilmentCode.Confusion => SkillCode.Confusion,
                AilmentCode.Paralysis => SkillCode.Paralysis,
                AilmentCode.EnemyParalysis => SkillCode.Paralysis,
                _ => throw new ArgumentOutOfRangeException(nameof(ailmentCode), ailmentCode, null)
            };

            return skillCode;
        }
    }
}