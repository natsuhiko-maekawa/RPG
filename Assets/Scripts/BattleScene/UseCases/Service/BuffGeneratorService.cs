using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class BuffGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly TargetDomainService _target;

        public BuffGeneratorService(
            OrderedItemsDomainService orderedItems,
            TargetDomainService target)
        {
            _orderedItems = orderedItems;
            _target = target;
        }

        public BuffValueObject Generate(
            SkillCommonValueObject skillCommon,
            BuffParameterValueObject buffParameter)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var targetIdList = _target.Get(actorId, skillCommon.Range);

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