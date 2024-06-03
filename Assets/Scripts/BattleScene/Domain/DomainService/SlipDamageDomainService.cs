using BattleScene.Domain.Code;
using BattleScene.Domain.IRepository;
using Utility.Interface;

namespace BattleScene.Domain.DomainService
{
    public class SlipDamageDomainService
    {
        private readonly IRandomEx _randomEx;
        private readonly ISlipDamageRepository _slipDamageRepository;

        public SlipDamageDomainService(
            ISlipDamageRepository slipDamageRepository,
            IRandomEx randomEx)
        {
            _slipDamageRepository = slipDamageRepository;
            _randomEx = randomEx;
        }

        public void AdvanceAllTurn()
        {
            foreach (var slipDamageEntity in _slipDamageRepository.Select())
                slipDamageEntity.AdvanceTurn();
        }

        public int GetDamageAmount(SlipDamageCode slipDamageCode)
        {
            var slipDamageEntity = _slipDamageRepository.Select(slipDamageCode);

            var enemyIntelligence = slipDamageEntity.EnemyIntelligence;
            var playerIntelligence = slipDamageEntity.PlayerIntelligence;
            var damageRate = slipDamageEntity.DamageRate;
            return (int)(enemyIntelligence * enemyIntelligence / (float)playerIntelligence * damageRate)
                   + _randomEx.Range(0, 2);
        }
    }
}