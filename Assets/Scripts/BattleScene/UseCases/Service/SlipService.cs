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
    public class SlipService : ISkillElementService<SlipValueObject>
    {
        private readonly ActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly ICollection<SlipEntity, SlipCode> _slipDamageCollection;

        public SlipService(
            ActualTargetIdPickerService actualTargetIdPicker,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<SlipEntity, SlipCode> slipDamageCollection)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _battlePropertyFactory = battlePropertyFactory;
            _slipDamageCollection = slipDamageCollection;
        }

        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<SlipValueObject> slipParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var slipList = slipParameterList
                .Select(GetSlip)
                .ToList();
            return slipList;

            BattleEventValueObject GetSlip(SlipValueObject slipParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    targetIdList: targetIdList,
                    luckRate: slipParameter.LuckRate);

                var slip = BattleEventValueObject.CreateSlip(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    slipCode: slipParameter.SlipCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);
                return slip;
            }
        }

        public void Register(BattleEventValueObject slip)
        {
            if (slip.ActualTargetIdList.Count == 0) return;
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;
            var slipEntity = new SlipEntity(
                slipCode: slip.SlipCode,
                effects: true,
                turn: slipDefaultTurn);
            _slipDamageCollection.Add(slipEntity);
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> slipValueObject)
        {
            foreach (var slip in slipValueObject) Register(slip);
        }
    }
}