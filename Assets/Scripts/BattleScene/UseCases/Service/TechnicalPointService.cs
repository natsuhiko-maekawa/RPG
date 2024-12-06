using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class TechnicalPointService : ITechnicalPointService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public TechnicalPointService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public int Get()
        {
            var player = _characterRepository.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = player.CurrentTechnicalPoint;
            return technicalPoint;
        }

        public void Reduce(SkillValueObject skill)
        {
            var player = _characterRepository.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = skill.Common.TechnicalPoint;
            player.CurrentTechnicalPoint -= technicalPoint;
        }

        public void Restore(IReadOnlyList<BattleEventEntity> restoreEventList)
        {
            foreach (var restoreEvent in restoreEventList)
            {
                var player = _characterRepository.Get()
                    .Single(x => x.IsPlayer);
                var technicalPoint = restoreEvent.TechnicalPoint;
                player.CurrentTechnicalPoint += technicalPoint;
            }
        }
    }
}