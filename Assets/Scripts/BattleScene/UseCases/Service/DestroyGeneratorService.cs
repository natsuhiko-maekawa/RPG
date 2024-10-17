using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DestroyGeneratorService : IPrimeSkillGeneratorService<DestroyParameterValueObject, DestroyValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly OrderedItemsDomainService _orderedItems;

        public DestroyGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
        }

        public IReadOnlyList<DestroyValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyParameterValueObject> destroyedParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var destroyList = destroyedParameterList.Select(GetDestroy).ToList();
            return destroyList;

            DestroyValueObject GetDestroy(DestroyParameterValueObject destroyedParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: destroyedParameter.LuckRate);

                var destroy = new DestroyValueObject(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList,
                    skillCode: skillCommon.SkillCode,
                    bodyPartCode: destroyedParameter.BodyPartCode,
                    destroyCount: destroyedParameter.Count);
                return destroy;
            }
        }
    }
}