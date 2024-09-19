using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffRegisterService _buffRegister;
        private readonly BuffGeneratorService _buffGenerator;
        private readonly BuffMessageState _buffMessageState;

        public BuffStateFactory(
            BattleLoggerService battleLogger,
            BuffRegisterService buffRegister,
            BuffGeneratorService buffGenerator,
            BuffMessageState buffMessageState)
        {
            _battleLogger = battleLogger;
            _buffRegister = buffRegister;
            _buffGenerator = buffGenerator;
            _buffMessageState = buffMessageState;
        }

        public BuffState Create(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffParameterValueObject> buffParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var buffList = _buffGenerator.Generate(
                skillCommon: skillCommon,
                buffParameterList: buffParameterList,
                targetIdList: targetIdList);
            var buffState = new BuffState(
                buffList: buffList,
                battleLogger: _battleLogger,
                buffRegister: _buffRegister,
                buffMessageState: _buffMessageState);
            return buffState;
        }
    }
}