using System.Collections.Generic;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class RestoreRegistererService : IPrimeSkillRegistererService<RestoreValueObject>
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public RestoreRegistererService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }
        
        public void Register(IReadOnlyList<RestoreValueObject> restoreList)
        {
            foreach (var restore in restoreList) AddTechnicalPoint(restore);
        }

        private void AddTechnicalPoint(RestoreValueObject restore)
        {
            var currentTechnicalPoint = _characterRepository.Select(restore.ActorId).CurrentTechnicalPoint;
            var technicalPoint = restore.TechnicalPoint;
            var newTechnicalPoint = currentTechnicalPoint + technicalPoint;
            _characterRepository.Select(restore.ActorId).CurrentTechnicalPoint = newTechnicalPoint;
        }
    }
}