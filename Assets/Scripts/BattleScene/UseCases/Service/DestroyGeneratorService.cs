using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DestroyGeneratorService : IPrimeSkillGeneratorService<DestroyParameterValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartCollection;
        private readonly IFactory<BodyPartPropertyValueObject, BodyPartCode> _bodyPartPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;

        public DestroyGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems,
            ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartCollection,
            IFactory<BodyPartPropertyValueObject, BodyPartCode> bodyPartPropertyFactory)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
            _bodyPartCollection = bodyPartCollection;
            _bodyPartPropertyFactory = bodyPartPropertyFactory;
        }

        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DestroyParameterValueObject> destroyedParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var destroyList = destroyedParameterList.Select(GetDestroy).ToList();
            return destroyList;

            PrimeSkillValueObject GetDestroy(DestroyParameterValueObject destroyedParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: destroyedParameter.LuckRate);

                var destroy = PrimeSkillValueObject.CreateDestroy(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList,
                    skillCode: skillCommon.SkillCode,
                    bodyPartCode: destroyedParameter.BodyPartCode,
                    destroyCount: destroyedParameter.Count);
                return destroy;
            }
        }
        
        public void Register(PrimeSkillValueObject destroy)
        {
            var bodyPartEntityList = destroy.ActualTargetIdList
                .Select(CreateBodyPartEntity)
                .ToList();
            _bodyPartCollection.Add(bodyPartEntityList);
            return;

            BodyPartEntity CreateBodyPartEntity(CharacterId targetId)
            {
                var bodyPartProperty = _bodyPartPropertyFactory.Create(destroy.BodyPartCode);
                var bodyPartEntity = new BodyPartEntity(
                    characterId: targetId,
                    bodyPartCode: destroy.BodyPartCode,
                    count: bodyPartProperty.Count);
                return bodyPartEntity;
            }
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> destroyList)
        {
            foreach (var destroy in destroyList) Register(destroy);
        }
    }
}