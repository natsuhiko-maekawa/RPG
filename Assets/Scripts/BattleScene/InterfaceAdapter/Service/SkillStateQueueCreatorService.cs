using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.State.Skill;

namespace BattleScene.InterfaceAdapter.Service
{
    public class SkillStateQueueCreatorService
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly AilmentStateFactory _ailmentStateFactory;
        private readonly BuffStateFactory _buffStateFactory;
        private readonly DamageStateFactory _damageStateFactory;
        private readonly RestoreStateFactory _restoreStateFactory;
        private readonly SlipStateFactory _slipStateFactory;
        
        private Queue<AbstractSkillState> Create(SkillCode skillCode, IReadOnlyList<CharacterId> targetIdList)
        {
            var skill = _skillFactory.Create(skillCode);
            var skillStates = Enumerable.Empty<AbstractSkillState>()
                .Concat(CreateAilmentState())
                .Concat(CreateDamageState())
                .Concat(CreateBuffState())
                .Concat(skill.RestoreParameterList.Select(x => _restoreStateFactory.Create(skill.SkillCommon, x)))
                .Concat(skill.SlipParameterList.Select(x => _slipStateFactory.Create(
                    skillCommon: skill.SkillCommon,
                    slipParameter: x,
                    targetIdList: targetIdList)));
            var skillStateQueue = new Queue<AbstractSkillState>(skillStates);
            return skillStateQueue;
            
            IEnumerable<AbstractSkillState> CreateAilmentState()
            {
                var ailmentStates = Enumerable.Empty<AbstractSkillState>();
                if (skill.AilmentParameterList.IsEmpty) return ailmentStates;
                var ailmentState = _ailmentStateFactory.Create(
                    skillCommon: skill.SkillCommon,
                    ailmentParameterList: skill.AilmentParameterList,
                    targetIdList: targetIdList);
                ailmentStates = ailmentStates.Append(ailmentState);
                return ailmentStates;
            }

            IEnumerable<AbstractSkillState> CreateDamageState()
            {
                var damageStates = skill.DamageParameterList
                    .Select(x => _damageStateFactory.Create(
                        skillCommon: skill.SkillCommon,
                        damageParameter: x,
                        targetIdList: targetIdList));
                return damageStates;
            }

            IEnumerable<AbstractSkillState> CreateBuffState()
            {
                var buffStates = Enumerable.Empty<AbstractSkillState>();
                if (skill.BuffParameterList.IsEmpty) return buffStates;
                var buffState = _buffStateFactory.Create(
                    skillCommon: skill.SkillCommon,
                    buffParameterList: skill.BuffParameterList,
                    targetIdList: targetIdList);
                buffStates = buffStates.Append(buffState);
                return buffStates;
            }
        }
    }
}