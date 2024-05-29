using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCase.Service
{
    public class SlipDamageService
    {
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly SlipDamageDomainService _slipDamage;
        
        public ResultEntity Damage()
        {
            var slipDamageCode = _orderedItems.FirstSlipDamageCode();
            var slipDamageAmount = _slipDamage.GetDamageAmount(slipDamageCode);
            var damage = new DamageValueObject(
                amount: slipDamageAmount,
                targetId: _characters.GetPlayerId());
            var slipDamageResult = new SlipDamageResultValueObject(
                slipDamageCode: slipDamageCode,
                damageList: ImmutableList.Create(damage));
            return _resultCreator.Create(slipDamageResult);
        }
    }
}