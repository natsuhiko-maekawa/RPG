using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public class AilmentId : AbstractId<AilmentId, HashId>
    {
        protected override HashId Id { get; }

        public AilmentId(
            CharacterId characterId,
            AilmentCode buffCode)
        {
            var tuple = (characterId, buffCode);
            Id = new HashId(tuple);
        }
    }
}