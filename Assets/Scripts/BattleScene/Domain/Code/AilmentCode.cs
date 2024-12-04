namespace BattleScene.Domain.Code
{
    // QUESTION: byteを指定してデータサイズを抑えた方がよいか
    public enum AilmentCode : byte
    {
        NoAilment,
        Blind,
        Deaf,
        Confusion,
        Paralysis,
        Sleep,
        Stun,
        Petrifaction,
        Curse,
        Binding,
        EnemyBlind,
        EnemyDeaf,
        EnemyParalysis
    }
}