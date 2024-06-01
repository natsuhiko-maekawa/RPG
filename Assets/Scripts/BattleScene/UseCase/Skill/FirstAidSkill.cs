using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     ファーストエイド
    /// </summary>
    internal class FirstAidSkill : AbstractSkill
    {
        private readonly FirstAidSkillElement _firstAidSkillElement;

        public FirstAidSkill(FirstAidSkillElement firstAidSkillElement)
        {
            _firstAidSkillElement = firstAidSkillElement;
        }

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Gun;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.FirstAidDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.RecoverDestroyedPartMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_firstAidSkillElement);
        }
    }
}