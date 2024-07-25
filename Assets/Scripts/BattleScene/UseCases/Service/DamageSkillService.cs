using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class DamageSkillService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
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

        public ResultEntity Execute(SkillValueObject skill, DamageValueObject damageInfo)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var damageList = new List<DamageResultValueObject>();
            for (var i = 0; i < damageInfo.AttackNumber; ++i)
            {
                var targetIdList = _target.Get(actorId, skill.Range);
                foreach (var targetId in targetIdList)
                {
                    var damage = new DamageResultValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, targetId, damageInfo),
                        isHit: _isHitEvaluator.Evaluate(actorId, targetId, damageInfo),
                        attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actorId, targetId, damageInfo),
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
        
        
        public ResultEntity Execute(SkillEntity skill)
        {
            // var actorId = _orderedItems.FirstCharacterId();
            // var damageSkill = (AbstractDamage)skill.FirstSkillService();
            // var damageList = new List<DamageValueObject>();
            // for (var i = 0; i < damageSkill.GetAttackNum(); ++i)
            // {
            //     var targetIdList = _target.Get(actorId, skill.AbstractSkill.GetRange());
            //     foreach (var targetId in targetIdList)
            //     {
            //         var damage = new DamageValueObject(
            //             damageSkill.GetDamageAmount(targetId),
            //             isHit: damageSkill.IsHit(targetId),
            //             attacksWeakPoint: damageSkill.AttacksWeakPoint(targetId),
            //             targetId: targetId,
            //             number: i);
            //         damageList.Add(damage);
            //     }
            // }
            //
            // damageList.Sort((x, y) => x.Number - y.Number);
            // var damageSkillResult = new DamageSkillResultValueObject(
            //     actorId,
            //     skill.SkillCode,
            //     damageList.ToImmutableList());
            //
            // return _resultCreator.Create(damageSkillResult);

            throw new NotImplementedException();
        }
    }
}