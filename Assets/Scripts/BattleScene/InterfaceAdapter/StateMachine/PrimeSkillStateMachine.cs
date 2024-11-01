﻿using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.State.PrimeSkill;
using BattleScene.InterfaceAdapter.State.Turn;
using VContainer;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class PrimeSkillStateMachine
    {
        private readonly IObjectResolver _container;
        private IEnumerator<IContext>? _primeSkillContextEnumerator;

        public PrimeSkillStateMachine(
            IObjectResolver container)
        {
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
            if (_primeSkillContextEnumerator!.Current?.IsContinue ?? false) return true;

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
            if (context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            var skill = context.Skill;

            // NOTE: 以下のif文の順序を変えないこと。
            // MoveNextで取得される順序がこの通りでなくてはならない。
            if (skill.DamageParameterList.Count != 0)
            {
                var damageContext = CreateContext(skill.DamageParameterList);
                yield return damageContext;
            }

            if (skill.AilmentParameterList.Count != 0)
            {
                var ailmentContext = CreateContext(skill.AilmentParameterList);
                yield return ailmentContext;
            }

            if (skill.SlipParameterList.Count != 0)
            {
                var slipContext = CreateContext(skill.SlipParameterList);
                yield return slipContext;
            }

            if (skill.DestroyedParameterList.Count != 0)
            {
                var destroyContext = CreateContext(skill.DestroyedParameterList);
                yield return destroyContext;
            }

            if (skill.BuffParameterList.Count != 0)
            {
                var buffContext = CreateContext(skill.BuffParameterList);
                yield return buffContext;
            }

            if (skill.EnhanceParameterList.Count != 0)
            {
                var enhanceContext = CreateContext(skill.EnhanceParameterList);
                yield return enhanceContext;
            }

            if (skill.ResetParameterList.Count != 0)
            {
                var resetContext = CreateContext(skill.ResetParameterList);
                yield return resetContext;
            }

            if (skill.RestoreParameterList.Count != 0)
            {
                var restoreContext = CreateContext(skill.RestoreParameterList);
                yield return restoreContext;
            }

            yield break;

            IContext CreateContext<TPrimeSkillParameter>(
                IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList)
            {
                if (context.ActorId is null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);

                var primeSkillStartState =
                    _container.Resolve<PrimeSkillStartState<TPrimeSkillParameter>>();
                var skillContext = new Context<TPrimeSkillParameter>(
                    primeSkillState: primeSkillStartState,
                    actorId: context.ActorId,
                    skillCommon: skill.Common,
                    primeSkillParameterList: primeSkillParameterList,
                    targetIdList: context.TargetIdList);
                return skillContext;
            }
        }
    }
}