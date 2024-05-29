using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    /// 空蝉
    /// </summary>
    internal class UtsusemiSkill : AbstractSkill
    {
        private readonly UtsusemiSkillElement _utsusemiSkillElement;

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
            throw new System.NotImplementedException();
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_utsusemiSkillElement);
        }
    }
}