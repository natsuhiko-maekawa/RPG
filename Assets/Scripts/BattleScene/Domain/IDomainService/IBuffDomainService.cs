using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IDomainService
{
    public interface IBuffDomainService
    {
        public float GetRate(CharacterId characterId, BuffCode buffCode);
    }
}