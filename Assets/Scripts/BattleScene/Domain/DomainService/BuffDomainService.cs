using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.DomainService
{
    public class BuffDomainService
    {
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;

        public BuffDomainService(
            IRepository<BuffEntity, BuffId> buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public float GetRate(CharacterId characterId, BuffCode buffCode)
        {
            var buffId = new BuffId(characterId, buffCode);
            var buffEntity = _buffRepository.Select(buffId);
            return buffEntity?.Rate ?? 1.0f;
        }

        public void AdvanceAllTurn(CharacterId characterId)
        {
            var buffEntityList = _buffRepository.Select()
                .Where(x => Equals(x.CharacterId, characterId))
                .ToImmutableList();
            foreach (var buffEntity in buffEntityList)
                buffEntity.AdvanceTurn();
            var recoverBuffEntityList = buffEntityList
                .Where(x => x.TurnIsEnd())
                .Select(x => x.Id)
                .ToImmutableList();
            _buffRepository.Delete(recoverBuffEntityList);
        }
    }
}