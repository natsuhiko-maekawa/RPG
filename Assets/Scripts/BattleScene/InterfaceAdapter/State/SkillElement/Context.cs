using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using JetBrains.Annotations;
using Utility;

namespace BattleScene.InterfaceAdapter.State.SkillElement
{
    public class Context<TPrimeSkillParameter> : IContext
    {
        private BaseState<TPrimeSkillParameter> _state = null!;

        public CharacterId ActorId { get; }
        public SkillCommonValueObject SkillCommon { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<TPrimeSkillParameter> PrimeSkillParameterList { get; }
        public Queue<BattleEventValueObject> BattleEventQueue { get; set; } = new();

        public Context(
            BaseState<TPrimeSkillParameter> primeSkillState,
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList)
        {
            ActorId = actorId;
            SkillCommon = skillCommon;
            TargetIdList = targetIdList;
            PrimeSkillParameterList = primeSkillParameterList;
            TransitionTo(primeSkillState);
        }

        public void TransitionTo(BaseState<TPrimeSkillParameter> skill)
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

        public bool IsContinue => _state is not IPrimeSkillStopState and not IPrimeSkillBreakState;

        public bool IsBreak => _state is IPrimeSkillBreakState;
    }
}