using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.Services;
using JetBrains.Annotations;
using Utility;

namespace BattleScene.Presenters.States.Skill
{
    public class Context<TSkill> : IContext
    {
        private BaseState<TSkill> _state = null!;

        public CharacterEntity Actor { get; }
        public SkillCommonValueObject SkillCommon { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<TSkill> SkillComponentList { get; }
        public Queue<BattleEventEntity> BattleEventQueue { get; set; } = new();
        public Dead Dead { get; set; }

        public Context(
            BaseState<TSkill> skillState,
            CharacterEntity actor,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<TSkill> skillComponentList,
            Dead dead)
        {
            Actor = actor;
            SkillCommon = skillCommon;
            TargetList = targetList;
            SkillComponentList = skillComponentList;
            Dead = dead;
            TransitionTo(skillState);
        }

        public void TransitionTo(BaseState<TSkill> skill)
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
            var className = TryGetMatchedText(originalClassName, "Skill(.+State).*$", out var matchedClassName)
                ? matchedClassName
                : originalClassName;
            var originalGenericName = _state.GetType().GenericTypeArguments.FirstOrDefault()?.Name ?? "";
            if (!TryGetMatchedText(originalGenericName, "(.+)ValueObject$", out var genericName))
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
        public bool IsContinue => _state is not ISkillStopState && !IsBreak;
        public bool IsBreak => _state is ISkillBreakState or ICharacterDeadState;

        public StateCode NextStateCode => _state switch
        {
            ISkillStopState => StateCode.Next,
            ISkillBreakState => StateCode.AdvanceTurnState,
            ICharacterDeadState => StateCode.CharacterDeadState,
            _ => StateCode.Next
        };
    }
}