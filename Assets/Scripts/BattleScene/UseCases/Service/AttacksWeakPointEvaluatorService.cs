using System;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AttacksWeakPointEvaluatorService
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;

        
        public bool Evaluate(CharacterId actorId, CharacterId targetId, AbstractDamage damage)
        {
            return damage.AttacksWeakPointEvaluationCode switch
            {
                AttacksWeakPointEvaluationCode.Basic => BasicEvaluate(targetId, damage),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool BasicEvaluate(CharacterId targetId, AbstractDamage damage)
        {
            return _characterRepository.Select(targetId).GetWeakPoints()
                .Intersect(damage.MatAttrCode)
                .Any();
        }
    }
}