namespace BattleScene.Presenters
{
    internal static class ExceptionMessage
    {
        public const string MementoStackSizeIsOver = "Size of MementoStack expected 4 or less but was over 4.";
        public const string ContextActorIdIsNull = "Context.ActorId expected any value but was null.";

        public const string ContextBattleEventQueueIsEmpty
            = "Context.BattleEventQueue expected not empty but was empty.";

        public const string ContextSkillIsNull = "Context.Skill expected any value but was null.";
    }
}