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
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(enhanceEventList.Count == enhanceList.Count);
            foreach (var (battleEvent, enhance) in enhanceEventList
                         .Zip(enhanceList, (battleEvent, enhance) => (battleEvent, enhance)))
            {
                battleEvent.UpdateEnhance(
                    enhanceCode: enhance.EnhanceCode,
                    effectTurn: enhance.Turn,
                    lifetimeCode: enhance.LifetimeCode,
                    targetList: targetList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> enhanceEventList)
        {
            foreach (var enhanceEvent in enhanceEventList)
            {
                foreach (var target in enhanceEvent.ActualTargetList)
                {
                    var enhance = _enhanceRepository.Get((target.Id, enhanceEvent.EnhanceCode));
                    enhance.Set(
                        turn: enhanceEvent.EffectTurn,
                        lifetimeCode: enhanceEvent.LifetimeCode);
                }
            }
        }
    }
}