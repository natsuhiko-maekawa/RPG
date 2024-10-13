using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;

namespace BattleScene.Domain.DomainService
{
    public class BuffDomainService : IBuffDomainService
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;

        public BuffDomainService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository)
        {
            _buffRepository = buffRepository;
        }
        
        public float GetRate(CharacterId characterId, BuffCode buffCode)
        {
            var buffEntity = _buffRepository.Select((characterId, buffCode));
            return buffEntity?.Rate ?? 1.0f;
        }
    }
}