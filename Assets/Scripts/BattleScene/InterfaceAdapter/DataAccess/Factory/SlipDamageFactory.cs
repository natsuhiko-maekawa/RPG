using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class SlipDamageFactory// : ISlipDamageFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ISlipDamageScriptableObject _slipDamageScriptableObject;

        public SlipDamageFactory(
            ICharacterRepository characterRepository,
            ISlipDamageScriptableObject slipDamageScriptableObject)
        {
            _characterRepository = characterRepository;
            _slipDamageScriptableObject = slipDamageScriptableObject;
        }

        public SlipDamageEntity Create(CharacterId playerId, CharacterId enemyId, SlipDamageCode slipDamageCode)
        {
            var slipDamageDto = _slipDamageScriptableObject.Get(slipDamageCode);
            var slipDamageEntityDto = new SlipDamageEntityDto(
                slipDamageDto.damageRate,
                _characterRepository.Select(playerId).Property.Intelligence,
                _characterRepository.Select(enemyId).Property.Intelligence);

            return new SlipDamageEntity(
                slipDamageCode,
                slipDamageEntityDto,
                new TurnValueObject(slipDamageDto.intervalTurn));
        }
    }
}