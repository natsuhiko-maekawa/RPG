using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class SlipGeneratorService : IPrimeSkillGeneratorService<SlipParameterValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ICollection<SlipEntity, SlipCode> _slipDamageCollection;

        public SlipGeneratorService(
            ActualTargetIdPickerService actualTargetIdPicker,
            OrderedItemsDomainService orderedItems,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<SlipEntity, SlipCode> slipDamageCollection)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _orderedItems = orderedItems;
            _battlePropertyFactory = battlePropertyFactory;
            _slipDamageCollection = slipDamageCollection;
        }

        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<SlipParameterValueObject> slipParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var value = _orderedItems.First().TryGetCharacterId(out var actorId);
            MyDebug.Assert(value);

            var slipList = slipParameterList
                .Select(GetSlip)
                .ToList();
            return slipList;

            PrimeSkillValueObject GetSlip(SlipParameterValueObject slipParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: slipParameter.LuckRate);

                var slip = PrimeSkillValueObject.CreateSlip(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    slipCode: slipParameter.SlipCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);
                return slip;
            }
        }
        
        public void Register(PrimeSkillValueObject slip)
        {
            if (slip.ActualTargetIdList.Count == 0) return;
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;
            var slipEntity = new SlipEntity(
                slipCode: slip.SlipCode,
                effects: true,
                turn: slipDefaultTurn);
            _slipDamageCollection.Add(slipEntity);
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> slipValueObject)
        {
            foreach (var slip in slipValueObject) Register(slip);
        }
    }
}