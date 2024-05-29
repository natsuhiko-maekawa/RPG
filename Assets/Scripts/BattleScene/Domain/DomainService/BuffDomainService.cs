using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class BuffDomainService
    {
        private readonly IBuffRepository _buffRepository;

        public BuffDomainService(
            IBuffRepository buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public bool Exists(CharacterId characterId, BuffCode buffCode)
        {
            return _buffRepository.Select(characterId, buffCode) != null;
        }
        
        public float GetRate(CharacterId characterId, BuffCode buffCode)
        {
            var buffEntity = _buffRepository.Select(characterId, buffCode);
            return buffEntity?.Rate ?? 1.0f;
        }
        
        public void AdvanceAllTurn(CharacterId characterId)
        {
            var buffEntityList = _buffRepository.Select(characterId);
            foreach (var buffEntity in buffEntityList)
                buffEntity.AdvanceTurn();
            var recoverBuffEntityList = buffEntityList
                .Where(x => x.TurnIsEnd())
                .ToImmutableList();
            _buffRepository.Delete(recoverBuffEntityList);
        }
    }
}