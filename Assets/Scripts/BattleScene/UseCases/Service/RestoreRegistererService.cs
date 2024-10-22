using System.Collections.Generic;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    // TODO: 削除すること
    public class RestoreRegistererService : IPrimeSkillRegistererService<PrimeSkillValueObject>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public RestoreRegistererService(
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterCollection = characterCollection;
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> restoreList)
        {
            foreach (var restore in restoreList) AddTechnicalPoint(restore);
        }

        private void AddTechnicalPoint(PrimeSkillValueObject restore)
        {
            var currentTechnicalPoint = _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint;
            var technicalPoint = restore.TechnicalPoint;
            var newTechnicalPoint = currentTechnicalPoint + technicalPoint;
            _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint = newTechnicalPoint;
        }
    }
}