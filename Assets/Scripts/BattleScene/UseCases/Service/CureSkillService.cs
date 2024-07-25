using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Interface;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class CureSkillService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly TargetDomainService _target;

        public CureSkillService(
            OrderedItemsDomainService orderedItems,
            ResultCreatorDomainService resultCreator,
            TargetDomainService target)
        {
            _orderedItems = orderedItems;
            _resultCreator = resultCreator;
            _target = target;
        }

        public ResultEntity Execute(SkillEntity skill)
        {
            // var actorId = _orderedItems.FirstCharacterId();
            // var cureSkill = (ICureSkill)skill.FirstSkillService();
            // var cureList = _target.Get(actorId, skill.Skill.Range)
            //     .Select(x => new CureResultValueObject(
            //         cureSkill.GetCureAmount(x),
            //         x))
            //     .ToImmutableList();
            //
            // var cureSkillResult = new CureSkillResultValueObject(
            //     actorId,
            //     skill.SkillCode,
            //     cureList);
            //
            // return _resultCreator.Create(cureSkillResult);
            throw new NotImplementedException();
        }
    }
}