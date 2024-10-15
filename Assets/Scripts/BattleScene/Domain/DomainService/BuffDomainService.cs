using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;

namespace BattleScene.Domain.DomainService
{
    public class BuffDomainService : IBuffDomainService
    {
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;

        public BuffDomainService(
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection)
        {
            _buffCollection = buffCollection;
        }
        
        public float GetRate(CharacterId characterId, BuffCode buffCode)
        {
            var buffEntity = _buffCollection.Get((characterId, buffCode));
            return buffEntity?.Rate ?? 1.0f;
        }
    }
}