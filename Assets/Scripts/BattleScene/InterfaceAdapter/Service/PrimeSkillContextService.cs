using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Battle;
using BattleScene.InterfaceAdapter.State.PrimeSkill;
using VContainer;

namespace BattleScene.InterfaceAdapter.Service
{
    public class PrimeSkillContextService
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IObjectResolver _container;
        private IEnumerator<IContext> _primeSkillContextEnumerator;
        private IContext _primeContext;

        public PrimeSkillContextService(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IObjectResolver container)
        {
            _skillFactory = skillFactory;
            _container = container;
        }

        public void Start(Context context)
        {
            _primeSkillContextEnumerator = GetContext(context).GetEnumerator();
        }

        public bool Select()
        {
            if (_primeContext == null) return MoveNextOrDispose();
            _primeContext.Select();
            return !_primeContext.HasEndState() || MoveNextOrDispose();
        }

        private bool MoveNextOrDispose()
        {
            var value = _primeSkillContextEnumerator.MoveNext();
            if (value)
            {
                _primeContext = _primeSkillContextEnumerator.Current;
            }
            else
            {
                _primeContext = null;
                _primeSkillContextEnumerator.Dispose();
            }

            return value;
        }
        
        private IEnumerable<IContext> GetContext(Context context)
        {
            var skill = _skillFactory.Create(context.SkillCode);

            if (skill.AilmentParameterList.Count != 0)
            {
                var ailmentContext
                    = CreateContext<AilmentParameterValueObject, AilmentValueObject>(skill.AilmentParameterList);
                yield return ailmentContext;
            }

            if (skill.DestroyedParameterList.Count != 0)
            {
                var destroyContext
                    = CreateContext<DestroyedParameterValueObject, DestroyedPartValueObject>(skill.DestroyedParameterList);
                yield return destroyContext;
            }

            if (skill.DamageParameterList.Count != 0)
            {
                var damageContext = CreateContext<DamageParameterValueObject, DamageValueObject>(skill.DamageParameterList);
                yield return damageContext;
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

            if (skill.SlipParameterList.Count != 0)
            {
                var slipContext = CreateContext<SlipParameterValueObject, SlipValueObject>(skill.SlipParameterList);
                yield return slipContext;
            }
            
            yield break;
            
            IContext CreateContext<TPrimeSkillParameter, TPrimeSkill>(
                IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList)
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