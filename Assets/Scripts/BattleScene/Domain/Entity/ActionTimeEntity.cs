using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class ActionTimeEntity
    {
        public CharacterId CharacterId { get; }
        public int ActionTime { get; private set; }

        public ActionTimeEntity(CharacterId characterId)
        {
            CharacterId = characterId;
        }

        public void Add(int time)
        {
            ActionTime += time;
        }
        
        public void Reduce(int time)
        {
            ActionTime -= time;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var actionTimeEntity = (ActionTimeEntity)obj;
            return CharacterId == actionTimeEntity.CharacterId;
        }
        
        public override int GetHashCode()
        {
            return CharacterId.GetHashCode();
        }
    }
}