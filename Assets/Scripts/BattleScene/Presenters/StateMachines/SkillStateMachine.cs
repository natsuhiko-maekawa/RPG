using System;
using System.Collections.Generic;
using BattleScene.Presenters.States;
using BattleScene.Presenters.States.Skill;
using BattleScene.Presenters.States.Turn;
using BattleScene.UseCases.Services;
using VContainer;

namespace BattleScene.Presenters.StateMachines
{
    public class SkillStateMachine
    {
        private readonly IObjectResolver _container;
        private IEnumerator<IContext>? _skillContextEnumerator;

        public SkillStateMachine(
            IObjectResolver container)
        {
            _container = container;
        }

        public bool TryOnSelect(Context context, out StateCode nextStateCode)
        {
            nextStateCode = StateCode.None;
            if (_skillContextEnumerator is null)
            {
                _skillContextEnumerator = GetContext(context).GetEnumerator();
                var value = TryMoveNextElseDispose(out nextStateCode);
                return value;
            }

            _skillContextEnumerator.Current?.Select();

            return TryMoveNextElseDispose(out nextStateCode);
        }

        private bool TryMoveNextElseDispose(out StateCode nextStateCode)
        {
            nextStateCode = StateCode.None;
            // 麻痺状態のスキルのようにスキルコンポーネントを持たないスキルも存在するため、Currentはnullの可能性がある
            if (_skillContextEnumerator!.Current is { IsContinue: true }) return true;

            if (_skillContextEnumerator.Current is not { IsBreak: true }
                && _skillContextEnumerator.MoveNext())
            {
                return TryMoveNextElseDispose(out nextStateCode);
            }

            nextStateCode =
                _skillContextEnumerator!.Current is null or { NextStateCode: StateCode.None or StateCode.Next }
                    ? GetNextStateCode()
                    : _skillContextEnumerator.Current.NextStateCode;
            _skillContextEnumerator.Dispose();
            _skillContextEnumerator = null;
            return false;
        }

        private StateCode GetNextStateCode()
        {
            var nextStateCode = _skillContextEnumerator!.Current is null or { Dead: Dead.None }
                ? StateCode.AdvanceTurnState
                : StateCode.CharacterDeadState;
            return nextStateCode;
        }

        // QUESTION: イテレータの使い方として正しいかどうかわからない。
        // ReSharper disable once CognitiveComplexity
        private IEnumerable<IContext> GetContext(Context turnContext)
        {
            if (turnContext.Skill is null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            IContext? prevSkillContext = null;
            var skill = turnContext.Skill;

            // NOTE: 以下のif文の順序を変えないこと。
            // MoveNextで取得される順序がこの通りでなくてはならない。
            if (skill.DamageList.Count != 0) yield return CreateContext(skill.DamageList);
            if (skill.AilmentList.Count != 0) yield return CreateContext(skill.AilmentList);
            if (skill.SlipList.Count != 0) yield return CreateContext(skill.SlipList);
            if (skill.DestroyList.Count != 0) yield return CreateContext(skill.DestroyList);
            if (skill.BuffList.Count != 0) yield return CreateContext(skill.BuffList);
            if (skill.EnhanceList.Count != 0) yield return CreateContext(skill.EnhanceList);
            if (skill.RecoveryList.Count != 0) yield return CreateContext(skill.RecoveryList);
            if (skill.CureList.Count != 0) yield return CreateContext(skill.CureList);
            if (skill.RestoreList.Count != 0) yield return CreateContext(skill.RestoreList);

            yield break;

            IContext CreateContext<TSkill>(
                IReadOnlyList<TSkill> skillList)
            {
                if (turnContext.Actor is null)
                    throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
                var skillStartState = _container.Resolve<SkillStartState<TSkill>>();
                var skillContext = new Context<TSkill>(
                    skillState: skillStartState,
                    actor: turnContext.Actor,
                    skillCommon: skill.Common,
                    skillComponentList: skillList,
                    targetList: turnContext.TargetList,
                    dead: prevSkillContext?.Dead ?? Dead.None);
                prevSkillContext = skillContext;
                return skillContext;
            }
        }
    }
}