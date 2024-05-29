using System.Collections.Immutable;
using BattleScene.Domain.Code;
using static BattleScene.Domain.Code.PlayerImageCode;

namespace BattleScene.UseCase
{
    public static class Constant
    {
        public const int MaxAgility = 256; // Agilityの最大値

        public const int ArmNumber = 2;
        public const int LegNumber = 2;
        public const int StomachNumber = 1;
        
        // 状態異常系
        public const int Interval = 5;
        public const int EnemyEffectiveTurn = 5;
        public const float AilmentsDmgRate = 1.2f;

        // エラーメッセージ系
        public const string CreateSaveData = "新規にセーブデータを作成しました。";
        public const string FailedLoadSaveData = "セーブデータの読み込みに失敗ました。";
        public const string InvalidSaveDataValue = "セーブデータの値が不正だったため、初期値を設定しました。";
        public const string FailedCreateInstance = "インスタンスの作成に失敗しました。";
        public const string FailedCreateSkillInstance = "スキルのインスタンスの作成に失敗しました。";
        public const string FailedLoadSkillInstance = "スキルのインスタンスの読み込みに失敗しました。";
        public const string NoMessageSettings = "メッセージが設定されていません";
        
        // GUI系
        public const PlayerImageCode DefaultImage = Katana;
        public const int SelectActionSlotNumber = 4;
        public const int SelectSkillSlotNumber = 4; // スキル選択画面の最大表示行数
        public const string SelectActionInfo = "\u21c5で選択 / Spaceで決定";
        public const string SelectSkillInfo = "\u21c5で選択 / Spaceで決定 / Cで戻る";
        public const string SelectTargetInfo = "\u21c4で選択 / Spaceで決定 / Cで戻る";

        public static readonly ImmutableList<string> ActionList
            = ImmutableList.Create("ATTACK", "SKILL", "DEFENCE", "FATALITY");
        public static readonly ImmutableList<string> OptionList = ImmutableList.Create("戦闘を続ける", "戦闘をやめる");
    }
}