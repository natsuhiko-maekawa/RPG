using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using JetBrains.Annotations;
using Utility;

namespace BattleScene.InterfaceAdapter.States.Skill
{
    public class Context<TSkillElement> : IContext
    {
        private BaseState<TSkillElement> _state = null!;

        public CharacterEntity Actor { get; }
        public SkillCommonValueObject SkillCommon { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<TSkillElement> SkillElementList { get; }
        public Queue<BattleEventEntity> BattleEventQueue { get; set; } = new();

        public Context(
            BaseState<TSkillElement> skillElementState,
            CharacterEntity actor,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<TSkillElement> skillElementList)
        {
            Actor = actor;
            SkillCommon = skillCommon;
            TargetList = targetList;
            SkillElementList = skillElementList;
            TransitionTo(skillElementState);
        }

        public void TransitionTo(BaseState<TSkillElement> skill)
        {
            _state = skill;
            MyDebug.Log(GetClassName());
            _state.SetContext(this);
            _state.Start();
        }

#if UNITY_EDITOR
        private string GetClassName()
        {
            var originalClassName = _state.GetType().Name;
            var className = TryGetMatchedText(originalClassName, "PrimeSkill(.+State).*$", out var matchedClassName)
                ? matchedClassName
                : originalClassName;
            var originalGenericName = _state.GetType().GenericTypeArguments.FirstOrDefault()?.Name ?? "";
            if (!TryGetMatchedText(originalGenericName, "(.+)ParameterValueObject$", out var genericName))
                return className;
            var genericClassName = genericName + className;
            return genericClassName;
        }

        private bool TryGetMatchedText(string input, [RegexPattern] string pattern, [NotNullWhen(true)]out string? matchedText)
        {
            var match = Regex.Match(input, pattern);
            matchedText = match.Success
                ? match.Groups[1].Value
                : null;
            return match.Success;
        }
#endif

        public void Select() => _state.Select();
        public bool IsContinue => _state is not ISkillElementStopState && !IsBreak;
        public bool IsBreak => _state is ISkillElementBreakState or ICharacterDeadState;

        public StateCode NextStateCode => _state switch
        {
            ISkillElementStopState => StateCode.AdvanceTurnState,
            ISkillElementBreakState => StateCode.AdvanceTurnState,
            ICharacterDeadState => StateCode.CharacterDeadState,
            _ => StateCode.Next
        };
    }
}