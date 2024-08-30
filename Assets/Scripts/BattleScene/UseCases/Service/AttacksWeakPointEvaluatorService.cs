using System;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AttacksWeakPointEvaluatorService
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;

        public AttacksWeakPointEvaluatorService(
            IRepository<CharacterAggregate, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public bool Evaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            return damageParameter.AttacksWeakPointEvaluationCode switch
            {
                AttacksWeakPointEvaluationCode.Basic => BasicEvaluate(targetId, damageParameter),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool BasicEvaluate(CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            return _characterRepository.Select(targetId).GetWeakPoints()
                .Intersect(damageParameter.MatAttrCode)
                .Any();
        }
    }
}