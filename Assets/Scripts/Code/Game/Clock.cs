public static class Clock
{
    public static float Time;
    public static bool IsPassTime(this float pivot, float wait)
    {
        return Time >= pivot + wait;
    }
    public static bool IsPassTime(this float pivot)
    {
        return Time >= pivot;
    }
    public static void Reset()
    {
        Time = 0;
    }
    public static void AddTime(float add)
    {
        Time += add;
    }
    public static void WaitForPass(System.Action onPass,float wait)
    {
        var time = Clock.Time;
        Wait.Until(onPass, () => time.IsPassTime(wait));
    }

}