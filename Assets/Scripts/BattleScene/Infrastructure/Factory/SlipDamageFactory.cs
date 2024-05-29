using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.Infrastructure.IScriptableObject;

namespace BattleScene.Infrastructure.Factory
{
    public class SlipDamageFactory
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
                DamageRate: slipDamageDto.damageRate,
                PlayerIntelligence: _characterRepository.Select(playerId).Property.Intelligence,
                EnemyIntelligence: _characterRepository.Select(enemyId).Property.Intelligence);

            return new SlipDamageEntity(
                slipDamageCode: slipDamageCode,
                dto: slipDamageEntityDto,
                turn: new TurnValueObject(slipDamageDto.intervalTurn));
        }
    }
}