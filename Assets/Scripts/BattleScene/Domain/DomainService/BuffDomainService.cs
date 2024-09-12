using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class BuffDomainService
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;

        public BuffDomainService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public void Add(BuffValueObject buff)
        {
            var turn = new TurnValueObject(buff.Turn);
            var buffList = buff.TargetIdList
                .Select(x => new BuffEntity(
                    characterId: x,
                    buffCode: buff.BuffCode,
                    rate: buff.Rate,
                    turn: turn))
                .ToImmutableList();
            _buffRepository.Update(buffList);
        }
        
        public float GetRate(CharacterId characterId, BuffCode buffCode)
        {
            var buffEntity = _buffRepository.Select((characterId, buffCode));
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