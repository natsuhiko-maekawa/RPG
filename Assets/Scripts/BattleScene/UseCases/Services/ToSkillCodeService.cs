using System;
using BattleScene.Domain.Codes;

namespace BattleScene.UseCases.Services
{
    public class ToSkillCodeService
    {
        public SkillCode From(AilmentCode ailmentCode)
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