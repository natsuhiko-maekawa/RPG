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
            IReadOnlyList<CharacterId> targetIdList)
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
                    targetIdList: targetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> buffEventList)
        {
            foreach (var buff in buffEventList)
            {
                foreach (var characterId in buff.TargetIdList)
                {
                    var buff1 = _buffRepository.Get((characterId, buff.BuffCode));
                    buff1.Set(
                        turn: buff.EffectTurn,
                        rate: buff.Rate,
                        lifetimeCode: buff.LifetimeCode);
                }
            }
        }
    }
}