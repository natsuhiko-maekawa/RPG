using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class LightningDamage : AbstractDamage
    {
        public override ImmutableList<MatAttrCode> MatAttrCode { get; } =
            ImmutableList.Create(Domain.Code.MatAttrCode.Lightning);
    }
}