using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.View.DigitView.OutputData;

namespace BattleScene.UseCases.View.DigitView.OutputDataFactory
{
    public class DamageDigitOutputDataFactory
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly BattleLogDomainService _battleLog;
        private readonly PlayerDomainService _player;

        public DamageDigitOutputDataFactory(
            IRepository<CharacterEntity, CharacterId> characterRepository, 
            BattleLogDomainService battleLog, 
            PlayerDomainService player)
        {
            _characterRepository = characterRepository;
            _battleLog = battleLog;
            _player = player;
        }

        public ImmutableList<DigitOutputData> Create()
        {
            return _battleLog.GetLast().AttackList
                .Select(x => new DigitOutputData(
                    x.Number,
                    x.Amount,
                    !x.IsHit,
                    DigitType.DamageHp,
                    Equals(x.TargetId, _player.GetId()),
                    Equals(x.TargetId, _player.GetId())
                        ? default
                        : _characterRepository.Select(x.TargetId).Position))
                .ToImmutableList();
        }
    }
}