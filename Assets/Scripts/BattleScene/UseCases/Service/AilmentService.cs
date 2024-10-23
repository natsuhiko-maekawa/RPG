using System;
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
    public class AilmentService : IPrimeSkillService<AilmentParameterValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;

        public AilmentService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems,
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentCollection = ailmentCollection;
        }

        public IReadOnlyList<BattleEventValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();

            var ailmentList = primeSkillParameterList.Select(GetAilment).ToList();

            return ailmentList;

            BattleEventValueObject GetAilment(AilmentParameterValueObject ailmentParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: ailmentParameter.LuckRate);

                var ailment = BattleEventValueObject.CreateAilment(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    ailmentCode: ailmentParameter.AilmentCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);

                return ailment;
            }
        }
        
        public void Register(BattleEventValueObject ailment)
        {
            var ailmentProperty = _ailmentPropertyFactory.Create(ailment.AilmentCode);
            var ailmentEntityList = ailment.ActualTargetIdList
                .Select(x => new AilmentEntity(
                    ailmentCode: ailment.AilmentCode,
                    characterId: x,
                    effects: true,
                    turn: ailmentProperty.Turn,
                    isSelfRecovery: ailmentProperty.IsSelfRecovery))
                .ToList();
            _ailmentCollection.Add(ailmentEntityList);
        }

        public void Register(IReadOnlyList<BattleEventValueObject> ailmentList)
        {
            foreach (var ailment in ailmentList) Register(ailment);
        }
    }
}