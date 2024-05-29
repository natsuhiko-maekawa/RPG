using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.AbstractClass
{
    public abstract class AbstractSkill
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
        public abstract ImmutableList<ISkillElement> GetSkillService();
    }
}