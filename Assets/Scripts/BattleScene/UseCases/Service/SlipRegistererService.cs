using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class SlipRegistererService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<SlipDamageEntity, SlipDamageCode> _slipDamageRepository;

        public SlipRegistererService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<SlipDamageEntity, SlipDamageCode> slipDamageRepository)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _slipDamageRepository = slipDamageRepository;
        }

        public void Register(SlipValueObject slip)
        {
            if (slip.ActualTargetIdList.Count == 0) return;
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;
            var slipEntity = new SlipDamageEntity(
                slipDamageCode: slip.SlipDamageCode,
                turn: slipDefaultTurn);
            _slipDamageRepository.Update(slipEntity);
        }
    }
}