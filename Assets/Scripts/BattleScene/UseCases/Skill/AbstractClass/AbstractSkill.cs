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

        protected ImmutableList<AilmentSkillElement> AilmentSkillElementList {  get; set; }
        protected ImmutableList<SlipDamageElement> SlipDamageElementList { get; set; }
        protected ImmutableList<DestroyPartSkillElement> DestroyPartSkillElementList { get; set; }
        protected ImmutableList<DamageSkillElement> DamageSkillElementList { get; set; }
        protected ImmutableList<BuffSkillElement> BuffSkillElementList { get; set; }
        protected ImmutableList<CureSkillElement> CureSkillElementList { get; set; }
        protected ImmutableList<ResetSkillElement> ResetSkillElementList { get; set; }
        protected ImmutableList<RestoreTechnicalPointSkillElement> RestoreTechnicalPointSkillElementList { get; set; }

        [Obsolete]
        public ImmutableList<ISkillElement> GetSkillService()
        {
            throw new NotImplementedException();
        }
    }
}