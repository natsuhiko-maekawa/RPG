using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class SlipRegistererService: IPrimeSkillRegistererService<SlipValueObject>
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<SlipEntity, SlipDamageCode> _slipDamageRepository;

        public SlipRegistererService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<SlipEntity, SlipDamageCode> slipDamageRepository)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _slipDamageRepository = slipDamageRepository;
        }

        public void Register(SlipValueObject slip)
        {
            if (slip.ActualTargetIdList.Count == 0) return;
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;
            var slipEntity = new SlipEntity(
                slipDamageCode: slip.SlipDamageCode,
                effects: true,
                turn: slipDefaultTurn);
            _slipDamageRepository.Update(slipEntity);
        }

        public void Register(IReadOnlyList<SlipValueObject> slipValueObject)
        {
            foreach (var slip in slipValueObject) Register(slip);
        }
    }
}