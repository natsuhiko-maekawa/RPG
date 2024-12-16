namespace BattleScene.Domain.Codes
{
    public enum PlayerImageCode : byte
    {
        NoImage, // NOTE: 仮でKatanaのイラストを設定している。
        Attack, // TODO: 仮でKatanaのイラストを設定している。立ち絵を描くこと。
        Avoidance,
        Bleeding,
        Burning, // TODO: 立ち絵を描くこと。
        Confusion,
        Damaged,
        Defence, // TODO: 立ち絵を描くこと。
        Gun,
        Katana,
        Paralysis, // TODO: 仮でDamagedのイラストを設定している。立ち絵を描くこと。
        Poisoning,
        Suffocation
    }
}