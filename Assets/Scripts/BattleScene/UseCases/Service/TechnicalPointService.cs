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
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public TechnicalPointService(
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterCollection = characterCollection;
        }

        public int Get()
        {
            var player = _characterCollection.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = player.CurrentTechnicalPoint;
            return technicalPoint;
        }

        public void Reduce(SkillValueObject skill)
        {
            var player = _characterCollection.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = skill.Common.TechnicalPoint;
            player.CurrentTechnicalPoint -= technicalPoint;
        }
    }
}