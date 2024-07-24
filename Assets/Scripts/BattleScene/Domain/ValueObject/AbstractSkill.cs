using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public abstract class AbstractSkill : ISkill
    {
        public abstract SkillCode SkillCode { get; }
        public virtual int TechnicalPoint { get; } = 0;
        public virtual ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList<BodyPartCode>.Empty;
        public abstract Range Range { get; }
        public virtual PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.NoImage;
        public virtual MessageCode Description { get; } = MessageCode.NoMessage;
        public abstract MessageCode AttackMessageCode { get; }

        [Obsolete]
        public virtual int GetTechnicalPoint()
        {
            return 0;
        }

        [Obsolete]
        public virtual ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList<BodyPartCode>.Empty;
        }

        [Obsolete]
        public Range GetRange()
        {
            throw new NotImplementedException();
        }

        public virtual ImmutableList<AbstractAilment> AilmentList {  get; } = ImmutableList<AbstractAilment>.Empty;
        public virtual ImmutableList<AbstractSlipDamage> SlipDamageList { get;  }
        public virtual ImmutableList<AbstractDestroyPart> DestroyPartList { get; }
        public virtual ImmutableList<AbstractDamage> DamageList { get; }
        public virtual ImmutableList<AbstractBuff> BuffList { get; }
        public virtual ImmutableList<AbstractCure> CureList { get;  }
        public virtual ImmutableList<AbstractReset> ResetList { get; }
        public virtual ImmutableList<AbstractRestoreTechnicalPoint> RestoreTechnicalPointList { get; }

        [Obsolete]
        public ImmutableList<ISkillElement> GetSkillService()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISkillQueue<T>
    {
        
    }

    public class SkillQueue<T> : ISkillQueue<T>
    {
        private ImmutableQueue<T> _skillList;
        public bool IsEmpty { get; private set; }

        public SkillQueue(params T[] array)
        {
            _skillList = ImmutableQueue.Create(array);
            IsEmpty = _skillList.IsEmpty;
        }

        public T Pop()
        {
            var item = _skillList.Peek();
            _skillList = _skillList.Dequeue();
            return item;
        }
    }
}