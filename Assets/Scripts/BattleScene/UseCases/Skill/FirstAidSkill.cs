using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     ファーストエイド
    /// </summary>
    internal class FirstAidSkill : AbstractSkill
    {
        public FirstAidSkill(FirstAid firstAid)
        {
            ResetList = ImmutableList.Create<AbstractReset>(firstAid);
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
    }
}