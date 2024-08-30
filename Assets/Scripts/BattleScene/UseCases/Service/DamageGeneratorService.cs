using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class DamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultCreatorDomainService _resultCreator;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly TargetDomainService _target;

        public DamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            ResultCreatorDomainService resultCreator,
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            TargetDomainService target)
        {
            _orderedItems = orderedItems;
            _resultCreator = resultCreator;
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _target = target;
        }

        public DamageValueObject Generate(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var damageList = new List<AttackValueObject>();
            for (var i = 0; i < damageParameter.AttackNumber; ++i)
            {
                var targetIdList = _target.Get(actorId, skillCommon.Range);
                foreach (var targetId in targetIdList)
                {
                    var damage = new AttackValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, targetId, damageParameter),
                        isHit: _isHitEvaluator.Evaluate(actorId, targetId, damageParameter),
                        attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actorId, targetId, damageParameter),
                        targetId: targetId,
                        number: i);
                    damageList.Add(damage);
                }
            }

            damageList.Sort((x, y) => x.Number - y.Number);
            
            return new DamageValueObject(
                actorId,
                skillCommon.SkillCode,
                damageList.ToImmutableList());
        }
        
        public ResultEntity Execute(SkillValueObject skill, DamageParameterValueObject damageParameterInfo)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var damageList = new List<AttackValueObject>();
            for (var i = 0; i < damageParameterInfo.AttackNumber; ++i)
            {
                var targetIdList = _target.Get(actorId, skill.Range);
                foreach (var targetId in targetIdList)
                {
                    var damage = new AttackValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, targetId, damageParameterInfo),
                        isHit: _isHitEvaluator.Evaluate(actorId, targetId, damageParameterInfo),
                        attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actorId, targetId, damageParameterInfo),
                        targetId: targetId,
                        number: i);
                    damageList.Add(damage);
                }
            }

            damageList.Sort((x, y) => x.Number - y.Number);
            var damageSkillResult = new DamageValueObject(
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