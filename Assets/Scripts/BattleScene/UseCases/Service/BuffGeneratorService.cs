using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class BuffGeneratorService : IPrimeSkillGeneratorService<BuffParameterValueObject, BuffValueObject>
    {
        private readonly OrderedItemsDomainService _orderedItems;

        public BuffGeneratorService(
            OrderedItemsDomainService orderedItems)
        {
            _orderedItems = orderedItems;
        }

        public IReadOnlyList<BuffValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffParameterValueObject> buffParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if(_orderedItems.First().TryGetCharacterId(out var actorId)) MyDebug.Assert(actorId != null);

            var buffList = buffParameterList.Select(GetBuff).ToList();
            return buffList;
            
            BuffValueObject GetBuff(BuffParameterValueObject buffParameter)
            {
                return new BuffValueObject(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    skillCode: skillCommon.SkillCode,
                    buffCode: buffParameter.BuffCode,
                    rate: buffParameter.Rate,
                    turn: buffParameter.Turn,
                    lifetimeCode: buffParameter.LifetimeCode);
            }
        }
    }
}