using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    // TODO: 削除すること
    public class SlipRegistererService : IPrimeSkillRegistererService<PrimeSkillValueObject>
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly ICollection<SlipEntity, SlipCode> _slipDamageCollection;

        public SlipRegistererService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<SlipEntity, SlipCode> slipDamageCollection)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _slipDamageCollection = slipDamageCollection;
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