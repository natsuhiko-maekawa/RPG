using System.Collections.Generic;
using BattleScene.InterfaceAdapter.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public abstract class OutputPrimeSkillState<TPrimeSkillValueObject> : AbstractSkillState
    {
        private readonly IReadOnlyList<TPrimeSkillValueObject> _primeSkillList;
        private readonly IOutput _output;
    }
}