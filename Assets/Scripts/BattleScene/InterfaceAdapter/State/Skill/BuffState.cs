using System.Collections.Generic;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffState : AbstractSkillState
    {
        private readonly IReadOnlyList<BuffValueObject> _buffList;
        private readonly BattleLoggerService _battleLogger;
        private readonly BuffRegisterService _buffRegister;
        private readonly BuffMessageState _buffMessageState;

        public BuffState(
            IReadOnlyList<BuffValueObject> buffList,
            BattleLoggerService battleLogger,
            BuffRegisterService buffRegister,
            BuffMessageState buffMessageState)
        {
            _buffList = buffList;   
            _battleLogger = battleLogger;
            _buffRegister = buffRegister;
            _buffMessageState = buffMessageState;
        }

        public override void Start()
        {
            _buffRegister.Register(_buffList);
            _battleLogger.Log(_buffList);
            SkillContext.TransitionTo(_buffMessageState);
        }
    }
}