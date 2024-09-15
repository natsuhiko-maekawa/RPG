using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.HitPointBarView.OutputData;

namespace BattleScene.UseCases.View.HitPointBarView.OutputDataFactory
{
    public class HitPointBarOutputDataFactory
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        
        public HitPointBarOutputDataFactory(
            BattleLogDomainService battleLog,
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _battleLog = battleLog;
            _characterPropertyFactory = characterPropertyFactory;
            _characterRepository = characterRepository;
        }
        
        public ImmutableList<HitPointBarOutputData> Create()
        {
            return _battleLog.GetLast().AttackList
                .GroupBy(x => x.TargetId)
                .Where(x => x.Any(y => y.IsHit))
                .Select(x => x
                    .Select(y => (targetId: y.TargetId, digit: y.Amount))
                    .Aggregate((y, z) => (y.targetId, y.digit + z.digit)))
                .Select(x => new HitPointBarOutputData(
                    _characterRepository.Select(x.targetId).IsPlayer
                        ? CharacterOutputData.SetPlayer()
                        : CharacterOutputData.SetEnemy(_characterRepository.Select(x.targetId).Position),
                    _characterPropertyFactory.Crate(x.targetId).HitPoint,
                    _characterRepository.Select(x.targetId).CurrentHitPoint))
                .ToImmutableList();
        }
    }
}