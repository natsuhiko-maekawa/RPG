using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class SlipDamageService
    {
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly SlipDamageDomainService _slipDamage;

        public SlipDamageService(
            CharactersDomainService characters,
            OrderedItemsDomainService orderedItems,
            ResultCreatorDomainService resultCreator,
            SlipDamageDomainService slipDamage)
        {
            _characters = characters;
            _orderedItems = orderedItems;
            _resultCreator = resultCreator;
            _slipDamage = slipDamage;
        }

        public ResultEntity Damage()
        {
            var slipDamageCode = _orderedItems.FirstSlipDamageCode();
            var slipDamageAmount = _slipDamage.GetDamageAmount(slipDamageCode);
            var damage = new DamageResultValueObject(
                slipDamageAmount,
                _characters.GetPlayerId());
            var slipDamageResult = new SlipDamageResultValueObject(
                slipDamageCode,
                ImmutableList.Create(damage));
            return _resultCreator.Create(slipDamageResult);
        }
    }
}