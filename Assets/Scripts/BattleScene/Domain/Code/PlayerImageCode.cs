using System;

namespace BattleScene.Domain.Code
{
    public enum PlayerImageCode
    {
        NoImage,
        [Obsolete] ArmInjured,
        Avoidance,
        Bleeding,
        Confusion,
        Damaged, // TODO: 立ち絵を描く
        Defence, // TODO: 立ち絵を書く
        Gun,
        Katana,
        Poisoning,
        [Obsolete] StomachPunched,
        Suffocation
    }
}