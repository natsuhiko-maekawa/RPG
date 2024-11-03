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
    public class EnhanceService : IPrimeSkillService<EnhanceValueObject>
    {
        private readonly ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceCollection;

        public EnhanceService(
            ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceCollection)
        {
            _enhanceCollection = enhanceCollection;
        }

        public IReadOnlyList<BattleEventValueObject> Generate(
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

        public void Register(IReadOnlyList<BattleEventValueObject> enhanceList)
        {
            foreach (var enhance in enhanceList) Register(enhance);
        }

        private void Register(BattleEventValueObject enhance)
        {
            var enhanceList = enhance.ActualTargetIdList
                .Select(x => new EnhanceEntity(
                    characterId: x,
                    enhanceCode: enhance.EnhanceCode,
                    turn: enhance.Turn,
                    lifetimeCode: enhance.LifetimeCode))
                .ToList();
            _enhanceCollection.Add(enhanceList);
        }
    }
}