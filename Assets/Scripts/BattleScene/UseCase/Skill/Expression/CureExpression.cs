using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Skill.AbstractClass;
using Utility;

namespace BattleScene.UseCase.Skill.Expression
{
    internal class CureExpression
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly IRandomEx _randomEx;

        public CureExpression(
            ICharacterRepository characterRepository,
            IHitPointRepository hitPointRepository,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _hitPointRepository = hitPointRepository;
            _randomEx = randomEx;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, CureSkillElement cureSkillElement)
        {
            var actor = _characterRepository.Select(actorId);
            var restore = actor.Property.Wisdom * 8 + _randomEx.Range(0, 2);
            return _hitPointRepository.Select(actorId).GetRestore(restore);
        }
    }
}