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
    public class EnhanceService : ISkillElementService<EnhanceValueObject>
    {
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;

        public EnhanceService(
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository)
        {
            _enhanceRepository = enhanceRepository;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> enhanceEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<EnhanceValueObject> enhanceList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(enhanceEventList.Count == enhanceList.Count);
            foreach (var (battleEvent, enhance) in enhanceEventList
                         .Zip(enhanceList, (battleEvent, enhance) => (battleEvent, enhance)))
            {
                battleEvent.UpdateEnhance(
                    enhanceCode: enhance.EnhanceCode,
                    effectTurn: enhance.Turn,
                    lifetimeCode: enhance.LifetimeCode,
                    targetIdList: targetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> enhanceEventList)
        {
            foreach (var enhanceEvent in enhanceEventList)
            {
                foreach (var characterId in enhanceEvent.ActualTargetIdList)
                {
                    var enhance = _enhanceRepository.Get((characterId, enhanceEvent.EnhanceCode));
                    enhance.Set(
                        turn: enhanceEvent.Turn,
                        lifetimeCode: enhanceEvent.LifetimeCode);
                }
            }
        }
    }
}