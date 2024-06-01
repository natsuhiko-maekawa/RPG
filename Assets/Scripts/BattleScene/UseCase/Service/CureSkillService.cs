using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Service
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
            var actorId = _orderedItems.FirstCharacterId();
            var cureSkill = (ICureSkill)skill.FirstSkillService();
            var cureList = _target.Get(actorId, skill.AbstractSkill.GetRange())
                .Select(x => new CureValueObject(
                    cureSkill.GetCureAmount(x),
                    x))
                .ToImmutableList();

            var cureSkillResult = new CureSkillResultValueObject(
                actorId,
                skill.SkillCode,
                cureList);

            return _resultCreator.Create(cureSkillResult);
        }
    }
}