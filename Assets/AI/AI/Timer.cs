 static class Timer
{
    public static void DelayAction(System.Action action, float delay)
    {
        TimerUtility.Instance.DelayAction(action, delay);
    }

    public static void ResetAfter(System.Action resetAction, float delay)
    {
        TimerUtility.Instance.DelayAction(resetAction, delay);
    }
}
