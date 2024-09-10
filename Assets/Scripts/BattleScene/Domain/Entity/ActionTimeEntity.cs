using System;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    [Obsolete]
    public class ActionTimeEntity : BaseEntity<ActionTimeEntity, CharacterId>
    {
        public ActionTimeEntity(CharacterId characterId)
        {
            Id = characterId;
        }

        public override CharacterId Id { get; }
        public int ActionTime { get; private set; }

        public void Add(int time)
        {
            ActionTime += time;
        }

        public void Reduce(int time)
        {
            ActionTime -= time;
        }
    }
}