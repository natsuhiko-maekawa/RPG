using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

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

        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<EnhanceValueObject> enhanceParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var enhanceList = enhanceParameterList
                .Select(x => BattleEventValueObject.CreateEnhance(
                    enhanceCode: x.EnhanceCode,
                    skillCode: skillCommon.SkillCode,
                    actorId: actorId,
                    targetIdList: targetIdList,
                    turn: x.Turn,
                    lifetimeCode: x.LifetimeCode))
                .ToList();
            return enhanceList;
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> enhanceList)
        {
            foreach (var enhance in enhanceList) Register(enhance);
        }

        private void Register(BattleEventValueObject enhanceEvent)
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