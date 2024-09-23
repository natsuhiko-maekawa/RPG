using System.Linq;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class AilmentState : AbstractSkillState<AilmentParameterValueObject, AilmentValueObject>
    {
        private readonly IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> _ailment;
        private readonly AilmentParameterValueObject _ailmentParameter;
        private readonly AilmentMessageState _ailmentMessageState;

        public AilmentState(
            IPrimeSkill<AilmentParameterValueObject, AilmentValueObject> ailment,
            AilmentMessageState ailmentMessageState)
        {
            _ailment = ailment;
            _ailmentMessageState = ailmentMessageState;
        }

        public override void Start()
        {
            SkillContext.PrimeSkillList = _ailment.Commit(
                skillCommon: SkillContext.SkillCommon,
                primeSkillParameter: SkillContext.PrimeSkillParameterList,
                targetIdList: SkillContext.TargetIdList);
            var failure = SkillContext.PrimeSkillList
                .All(x => x.ActualTargetIdList.Count == 0);
            SkillContext.TransitionTo(_ailmentMessageState);
        }
    }
}