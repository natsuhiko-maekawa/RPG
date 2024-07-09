using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.Skill.Interface
{
    public interface IResetSkill
    {
        public ImmutableList<AilmentCode> GetResetAilmentList();
        public ImmutableList<SlipDamageCode> GetResetSlipDamageCodeList();
    }
}