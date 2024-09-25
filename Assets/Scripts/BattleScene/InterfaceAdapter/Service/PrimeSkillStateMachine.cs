using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.PrimeSkill;
using BattleScene.InterfaceAdapter.State.Turn;
using VContainer;

namespace BattleScene.InterfaceAdapter.Service
{
    public class PrimeSkillStateMachine
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IObjectResolver _container;
        private IEnumerator<IContext> _primeSkillContextEnumerator;

        public PrimeSkillStateMachine(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IObjectResolver container)
        {
            _skillFactory = skillFactory;
            _container = container;
        }
        
        public bool Select(Context context)
        {
            if (_primeSkillContextEnumerator == null)
            {
                _primeSkillContextEnumerator = GetContext(context).GetEnumerator();
                var value = MoveNextOrDispose();
                return value;
            }
            
            _primeSkillContextEnumerator.Current?.Select();
            
            return MoveNextOrDispose();
        }

        private bool MoveNextOrDispose()
        {
            if (_primeSkillContextEnumerator.Current?.IsContinue ?? false) return true;
            
            if (_primeSkillContextEnumerator.Current is not { IsBreak: true } 
                && _primeSkillContextEnumerator.MoveNext())
            {
                return MoveNextOrDispose();
            }

            _primeSkillContextEnumerator.Dispose();
            _primeSkillContextEnumerator = null;
            return false;
        }
        
        private IEnumerable<IContext> GetContext(Context context)
        {
            var skill = _skillFactory.Create(context.SkillCode);

            if (skill.DamageParameterList.Count != 0)
            {
                var damageContext = CreateContext<DamageParameterValueObject, DamageValueObject>(skill.DamageParameterList);
                yield return damageContext;
            }
            
            if (skill.AilmentParameterList.Count != 0)
            {
                var ailmentContext
                    = CreateContext<AilmentParameterValueObject, AilmentValueObject>(skill.AilmentParameterList);
                yield return ailmentContext;
            }
            
            if (skill.SlipParameterList.Count != 0)
            {
                var slipContext = CreateContext<SlipParameterValueObject, SlipValueObject>(skill.SlipParameterList);
                yield return slipContext;
            }

            if (skill.DestroyedParameterList.Count != 0)
            {
                var destroyContext
                    = CreateContext<DestroyParameterValueObject, DestroyValueObject>(skill.DestroyedParameterList);
                yield return destroyContext;
            }
            
            if (skill.BuffParameterList.Count != 0)
            {
                var buffContext = CreateContext<BuffParameterValueObject, BuffValueObject>(skill.BuffParameterList);
                yield return buffContext;
            }

            if (skill.RestoreParameterList.Count != 0)
            {
                var restoreContext =
                    CreateContext<RestoreParameterValueObject, RestoreValueObject>(skill.RestoreParameterList);
                yield return restoreContext;
            }
            
            yield break;
            
            IContext CreateContext<TPrimeSkillParameter, TPrimeSkill>(
                IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList) where TPrimeSkill : PrimeSkillValueObject
            {
                var primeSkillStartState = _container.Resolve<PrimeSkillStartState<TPrimeSkillParameter, TPrimeSkill>>();
                var skillContext = new Context<TPrimeSkillParameter, TPrimeSkill>(
                    primeSkillState: primeSkillStartState,
                    skillCommon: skill.SkillCommon,
                    primeSkillParameterList: primeSkillParameterList,
                    targetIdList: context.TargetIdList);
                return skillContext;
            }
        }
    }
}