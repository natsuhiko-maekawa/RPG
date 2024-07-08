using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     空蝉
    /// </summary>
    internal class UtsusemiSkill : AbstractSkill
    {
        public UtsusemiSkill(Utsusemi utsusemi)
        {
            BuffSkillElementList = ImmutableList.Create<AbstractBuff>(utsusemi);
        }

        public override int GetTechnicalPoint()
        {
            return 5;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.UtsusemiDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            throw new NotImplementedException();
        }
    }
}