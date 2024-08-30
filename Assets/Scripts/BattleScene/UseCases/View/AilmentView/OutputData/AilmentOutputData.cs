﻿using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.View.AilmentView.OutputData
{
    public record AilmentOutputData(
        CharacterId CharacterId,
        IList<AilmentCode> AilmentCodeList = default,
        IList<SlipDamageCode> SlipDamageCodeList = default);
}