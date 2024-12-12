using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.State;
using BattleScene.InterfaceAdapter.State.Skill;
using BattleScene.InterfaceAdapter.State.Turn;
using VContainer;

namespace BattleScene.InterfaceAdapter.StateMachine
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
            if (_skillContextEnumerator == null)
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
            if (_skillContextEnumerator!.Current?.IsContinue ?? false) return true;

            if (_skillContextEnumerator.Current is not { IsBreak: true }
                && _skillContextEnumerator.MoveNext())
            {
                return TryMoveNextElseDispose(out nextStateCode);
            }

            nextStateCode = _skillContextEnumerator.Current?.NextStateCode ?? StateCode.None;
            nextStateCode = nextStateCode == StateCode.None
                ? StateCode.AdvanceTurnState
                : nextStateCode;
            _skillContextEnumerator.Dispose();
            _skillContextEnumerator = null;
            return false;
        }

        // QUESTION: イテレータの使い方として正しいかどうかわからない。
        // ReSharper disable once CognitiveComplexity
        private IEnumerable<IContext> GetContext(Context context)
        {
            if (context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);
            var skill = context.Skill;

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

            IContext CreateContext<TSkillElement>(
                IReadOnlyList<TSkillElement> skillElementList)
            {
                if (context.Actor is null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);

                var skillElementStartState =
                    _container.Resolve<SkillElementStartState<TSkillElement>>();
                var skillContext = new Context<TSkillElement>(
                    skillElementState: skillElementStartState,
                    actor: context.Actor,
                    skillCommon: skill.Common,
                    skillElementList: skillElementList,
                    targetList: context.TargetList);
                return skillContext;
            }
        }
    }
}