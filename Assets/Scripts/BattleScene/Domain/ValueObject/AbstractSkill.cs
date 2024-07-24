using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public abstract class AbstractSkill : ISkill
    {
        public int TechnicalPoint { get; } = 0;

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

        public ImmutableList<AbstractAilment> AilmentList {  get; set; } = ImmutableList<AbstractAilment>.Empty;
        public ImmutableList<AbstractSlipDamage> SlipDamageList { get; set; }
        public ImmutableList<AbstractDestroyPart> DestroyPartList { get; set; }
        public ImmutableList<AbstractDamage> DamageList { get; set; }
        public ImmutableList<AbstractBuff> BuffList { get; set; }
        public ImmutableList<AbstractCure> CureList { get; set; }
        public ImmutableList<AbstractReset> ResetList { get; set; }
        public ImmutableList<AbstractRestoreTechnicalPoint> RestoreTechnicalPointList { get; set; }

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