using System;

namespace BattleScene.Domain.Codes
{
    [Flags]
    public enum MatAttrCode : byte
    {
        NoAttr = 0,
        Fire = 1,
        Ice = 2,
        Lightning = 4
    }
}