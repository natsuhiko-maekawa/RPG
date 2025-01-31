namespace BattleScene.UseCases
{
    internal static class ExceptionMessage
    {
        public const string AilmentCodeIsDefaultValue = "Ailment Code expected any value but was default value.";
        public const string ResetParameterIsNoSingle = "Reset parameter expected single but was multiple or empty.";
        public const string RestoreParameterIsNoSingle = "Restore parameter expected single but was multiple or empty.";

        public const string InvalidSaveDataValue = "セーブデータの値が不正だったため、初期値を設定しました。";
        public const string FailedCreateInstance = "インスタンスの作成に失敗しました。";
        public const string FailedCreateSkillInstance = "スキルのインスタンスの作成に失敗しました。";
        public const string FailedLoadSkillInstance = "スキルのインスタンスの読み込みに失敗しました。";
        public const string NoMessageSettings = "メッセージが設定されていません";
    }
}