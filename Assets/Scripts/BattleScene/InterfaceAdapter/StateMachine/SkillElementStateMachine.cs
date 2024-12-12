using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.State;
using BattleScene.InterfaceAdapter.State.SkillElement;
using BattleScene.InterfaceAdapter.State.Turn;
using VContainer;

namespace BattleScene.InterfaceAdapter.StateMachine
{
    public class SkillElementStateMachine
    {
        private readonly IObjectResolver _container;
        private IEnumerator<IContext>? _skillElementContextEnumerator;

        public SkillElementStateMachine(
            IObjectResolver container)
        {
            _container = container;
        }

        public bool TryOnSelect(Context context, out StateCode nextStateCode)
        {
            nextStateCode = StateCode.None;
            if (_skillElementContextEnumerator == null)
            {
                _skillElementContextEnumerator = GetContext(context).GetEnumerator();
                var value = TryMoveNextElseDispose(out nextStateCode);
                return value;
            }

            _skillElementContextEnumerator.Current?.Select();

            return TryMoveNextElseDispose(out nextStateCode);
        }

        private bool TryMoveNextElseDispose(out StateCode nextStateCode)
        {
            nextStateCode = StateCode.None;
            // 麻痺状態のスキルのようにスキルコンポーネントを持たないスキルも存在するため、Currentはnullの可能性がある
            if (_skillElementContextEnumerator!.Current?.IsContinue ?? false) return true;

            if (_skillElementContextEnumerator.Current is not { IsBreak: true }
                && _skillElementContextEnumerator.MoveNext())
            {
                return TryMoveNextElseDispose(out nextStateCode);
            }

            nextStateCode = _skillElementContextEnumerator.Current?.NextStateCode ?? StateCode.None;
            nextStateCode = nextStateCode == StateCode.None
                ? StateCode.AdvanceTurnState
                : nextStateCode;
            _skillElementContextEnumerator.Dispose();
            _skillElementContextEnumerator = null;
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

            if (skill.CureParameterList.Count != 0)
            {
                var cureContext = CreateContext(skill.CureParameterList);
                yield return cureContext;
            }

            if (skill.RestoreParameterList.Count != 0)
            {
                var restoreContext = CreateContext(skill.RestoreParameterList);
                yield return restoreContext;
            }

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