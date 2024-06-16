using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Service
{
    public class DamageSkillService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly TargetDomainService _target;

        public DamageSkillService(
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
            var damageSkill = (DamageSkillElement)skill.FirstSkillService();
            var damageList = new List<DamageValueObject>();
            for (var i = 0; i < damageSkill.GetAttackNum(); ++i)
            {
                var targetIdList = _target.Get(actorId, skill.AbstractSkill.GetRange());
                foreach (var targetId in targetIdList)
                {
                    var damage = new DamageValueObject(
                        damageSkill.GetDamageAmount(targetId),
                        isHit: damageSkill.IsHit(targetId),
                        attacksWeakPoint: damageSkill.AttacksWeakPoint(targetId),
                        targetId: targetId,
                        number: i);
                    damageList.Add(damage);
                }
            }

            damageList.Sort((x, y) => x.Number - y.Number);
            var damageSkillResult = new DamageSkillResultValueObject(
                actorId,
                skill.SkillCode,
                damageList.ToImmutableList());

            return _resultCreator.Create(damageSkillResult);
        }
    }
}