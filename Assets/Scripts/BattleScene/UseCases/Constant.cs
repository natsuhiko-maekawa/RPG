using System.Collections.Generic;

namespace BattleScene.UseCases
{
    public static class Constant
    {
        public const int AttackCountUpperLimit = 25;
        public const int MaxOrderNumber = 14;

        // エラーメッセージ系
        public const string CreateSaveData = "新規にセーブデータを作成しました。";
        public const string FailedLoadSaveData = "セーブデータの読み込みに失敗ました。";
        public const string InvalidSaveDataValue = "セーブデータの値が不正だったため、初期値を設定しました。";
        public const string FailedCreateInstance = "インスタンスの作成に失敗しました。";
        public const string FailedCreateSkillInstance = "スキルのインスタンスの作成に失敗しました。";
        public const string FailedLoadSkillInstance = "スキルのインスタンスの読み込みに失敗しました。";
        public const string NoMessageSettings = "メッセージが設定されていません";

        // GUI系
        public const string SelectActionInfo = "\u21c5で選択 / Spaceで決定";
        public const string SelectSkillInfo = "\u21c5で選択 / Spaceで決定 / Cで戻る";
        public const string SelectTargetInfo = "\u21c4で選択 / Spaceで決定 / Cで戻る";

        public static readonly IReadOnlyList<string> OptionList = new[] { "戦闘を続ける", "戦闘をやめる" };
    }
}