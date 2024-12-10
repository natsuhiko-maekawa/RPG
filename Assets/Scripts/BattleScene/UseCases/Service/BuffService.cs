using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class BuffService : ISkillElementService<BuffValueObject>
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