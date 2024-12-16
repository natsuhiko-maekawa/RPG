using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using Utility;

namespace BattleScene.Presenters.Services.Replacements
{
    public class BuffReplacementService : IReplacementService
    {
        public string Replacement => "[buff]";
        private readonly BattleLogDomainService _battleLog;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewInfoResource;

        public BuffReplacementService(
            BattleLogDomainService battleLog,
            IResource<BuffViewDto, BuffCode> buffViewInfoResource)
        {
            _battleLog = battleLog;
            _buffViewInfoResource = buffViewInfoResource;
        }

        public bool IsMatch(string value) => value == Replacement;

        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var buffCode = _battleLog.GetLast().BuffCode;
            MyDebug.Assert(buffCode != BuffCode.NoBuff);
            if (buffCode == BuffCode.NoBuff) return string.Empty;
            var buffName = _buffViewInfoResource.Get(buffCode).Name;
            return buffName;
        }
    }
}