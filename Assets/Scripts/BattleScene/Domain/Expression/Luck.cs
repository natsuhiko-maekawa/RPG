using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility.Interface;

namespace BattleScene.Domain.Expression
{
    public class Luck
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public bool IsLucky(CharacterId actorId, CharacterId targetId, ILuckSkillElement skill)
        {
            var actorLuck = _characterRepository.Select(actorId).Property.Luck;
            var targetLuck = _characterRepository.Select(targetId).Property.Luck;
            var rate = skill.GetLuckRate() * (1.0f + (actorLuck - targetLuck) / Threshold);
            return _randomEx.Probability(rate);
        }
    }
}