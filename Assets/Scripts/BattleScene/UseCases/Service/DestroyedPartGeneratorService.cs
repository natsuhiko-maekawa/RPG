using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class DestroyedPartGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRepository<BodyPartEntity, BodyPartId> _bodyPartRepository;
        private readonly IRandomEx _randomEx;
        private readonly TargetDomainService _target;

        public DestroyedPartValueObject Generate(
            SkillCommonValueObject skillCommon,
            DestroyedParameterValueObject destroyedParameter)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var targetIdList = _target.Get(actorId, skillCommon.Range)
                .Where(_ => _randomEx.Probability(destroyedParameter.LuckRate))
                .Where(x => _bodyPartRepository.Select()
                    .First(y => Equals(y.CharacterId, x) && y.BodyPartCode == destroyedParameter.BodyPartCode)
                    .IsAvailable())
                .ToImmutableList();

            return new DestroyedPartValueObject(
                actorId: actorId,
                targetIdList: targetIdList,
                skillCode: skillCommon.SkillCode,
                bodyPartCode: destroyedParameter.BodyPartCode,
                destroyCount: destroyedParameter.Count);
        }

        public BodyPartEntity Create(
            IList<BodyPartEntity> bodyPartList,
            DestroyedPartValueObject destroyedPart)
        {
            throw new NotImplementedException();
        }
    }
}