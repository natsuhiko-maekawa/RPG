using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.AbstractClass
{
    internal abstract class AbstractSkill : ISkill
    {
        public virtual int GetTechnicalPoint()
        {
            return 0;
        }

        public virtual ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList<BodyPartCode>.Empty;
        }

        public abstract Range GetRange();

        public virtual PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.NoImage;
        }

        public virtual MessageCode GetDescription()
        {
            return MessageCode.NoMessage;
        }

        public abstract MessageCode GetAttackMessage();

        protected ImmutableList<AbstractAilment> AilmentSkillElementList {  get; set; }
        protected ImmutableList<AbstractSlipDamage> SlipDamageElementList { get; set; }
        protected ImmutableList<AbstractDestroyPart> DestroyPartSkillElementList { get; set; }
        protected ImmutableList<AbstractDamage> DamageSkillElementList { get; set; }
        protected ImmutableList<AbstractBuff> BuffSkillElementList { get; set; }
        protected ImmutableList<AbstractCure> CureSkillElementList { get; set; }
        protected ImmutableList<AbstractReset> ResetSkillElementList { get; set; }
        protected ImmutableList<AbstractRestoreTechnicalPoint> RestoreTechnicalPointSkillElementList { get; set; }

        [Obsolete]
        public ImmutableList<ISkillElement> GetSkillService()
        {
            throw new NotImplementedException();
        }
    }
}