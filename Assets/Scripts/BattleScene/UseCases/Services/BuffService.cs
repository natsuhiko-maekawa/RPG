using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class BuffService : ISkillService<BuffValueObject>
    {
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;

        public BuffService(
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository)
        {
            _buffRepository = buffRepository;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> buffEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffValueObject> buffList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(buffEventList.Count == buffList.Count);
            foreach (var (battleEvent, buff) in buffEventList
                         .Zip(buffList, (battleEvent, buff) => (battleEvent, buff)))
            {
                battleEvent.UpdateBuff(
                    buffCode: buff.BuffCode,
                    effectTurn: buff.Turn,
                    rate: buff.Rate,
                    lifetimeCode: buff.LifetimeCode,
                    targetList: targetList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> buffEventList)
        {
            foreach (var buffEvent in buffEventList)
            {
                foreach (var target in buffEvent.TargetList)
                {
                    var buff = _buffRepository.Get((target.Id, buffEvent.BuffCode));
                    buff.Set(
                        turn: buffEvent.EffectTurn,
                        rate: buffEvent.Rate,
                        lifetimeCode: buffEvent.LifetimeCode);
                }
            }
        }
    }
}