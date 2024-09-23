using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class DestroyedPartGeneratorService
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;
        private readonly IRandomEx _randomEx;
        private readonly TargetDomainService _target;

        public DestroyedPartGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems,
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository,
            IRandomEx randomEx,
            TargetDomainService target)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
            _bodyPartRepository = bodyPartRepository;
            _randomEx = randomEx;
            _target = target;
        }

        public IReadOnlyList<DestroyedPartValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyedParameterValueObject> destroyedParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var destroyList = destroyedParameterList.Select(GetDestroy).ToList();
            return destroyList;

            DestroyedPartValueObject GetDestroy(DestroyedParameterValueObject destroyedParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: destroyedParameter.LuckRate);
                
                var destroy = new DestroyedPartValueObject(
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