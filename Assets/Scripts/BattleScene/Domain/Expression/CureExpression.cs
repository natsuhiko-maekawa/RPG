using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Utility.Interface;

namespace BattleScene.UseCases.Skill.Expression
{
    public class CureExpression
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRandomEx _randomEx;

        public CureExpression(
            ICharacterRepository characterRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
            _randomEx = randomEx;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, AbstractCure cure)
        {
            var actor = _characterRepository.Select(actorId);
            var restore = actor.Property.Wisdom * 8 + _randomEx.Range(0, 2);
            return _hitPointRepository.Select(actorId).GetRestore(restore);
        }
    }
}