using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill.AbstractClass
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

        protected ImmutableList<AbstractAilment> AilmentList {  get; set; }
        protected ImmutableList<AbstractSlipDamage> SlipDamageList { get; set; }
        protected ImmutableList<AbstractDestroyPart> DestroyPartList { get; set; }
        protected ImmutableList<AbstractDamage> DamageList { get; set; }
        protected ImmutableList<AbstractBuff> BuffList { get; set; }
        protected ImmutableList<AbstractCure> CureList { get; set; }
        protected ImmutableList<AbstractReset> ResetList { get; set; }
        protected ImmutableList<AbstractRestoreTechnicalPoint> RestoreTechnicalPointList { get; set; }

        [Obsolete]
        public ImmutableList<ISkillElement> GetSkillService()
        {
            throw new NotImplementedException();
        }
    }
}