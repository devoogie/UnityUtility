using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
public static partial class Wait
{
    public static void Frame(System.Action onComplete, int frame = 1, Segment segment = Segment.Update)
    {
        Timing.RunCoroutine(Frame(onComplete,frame),segment);
    }

    private static IEnumerator<float> Frame(System.Action onComplete,int frame)
    {
        for(int i =0;i<frame;i++)
            yield return Timing.WaitForOneFrame;
        onComplete();
    }

    public static void Second(System.Action onComplete, float second, Segment segment = Segment.Update)
    {
        Timing.RunCoroutine(Second(onComplete, second), segment);
    }

    public static void FixedSecond(System.Action onComplete, float second, Segment segment = Segment.FixedUpdate)
    {
        Timing.RunCoroutine(FixedSecond(onComplete, second), segment);
    }
    private static IEnumerator<float> Second(System.Action onComplete, float second)
    {
        yield return Timing.WaitForSeconds(second);
        onComplete();
    }

    static IEnumerator CorWaitFixedSecond(System.Action onComplete, float second)
    {
        float time = second;
        while (time > 0f)
        {
            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        onComplete();
    }
    
    private static IEnumerator<float> FixedSecond(System.Action onComplete, float second)
    {
        float time = second;
        //Debug.LogError($"BEFORE : {Time.realtimeSinceStartup}");
        //Debug.LogError($"BEFORE : {Timing.DeltaTime}");
        while (time > 0f)
        {
            time -= Time.deltaTime / (Time.timeScale / 1f);
            yield return Timing.WaitForOneFrame;
        }
        //Debug.LogError($"AFTER : {Time.realtimeSinceStartup}");
        onComplete();
    }
    
    public static void Until(System.Action onComplete, System.Func<bool> condition, Segment segment = Segment.Update)
    {
        Timing.RunCoroutine(WaitUntil(onComplete, condition), segment);
    }

    private static IEnumerator<float> WaitUntil(System.Action onComplete, System.Func<bool> condition)
    {
        yield return Timing.WaitUntilTrue(condition);
        onComplete();
    }
    public static void StartTimer(string timerKey, System.DateTimeOffset dateTime, System.Action onUpdate = null, System.Action onStop = null)
    {
        Timing.KillCoroutines(timerKey);
        Timing.RunCoroutine(startTimer(dateTime, onUpdate, onStop), Segment.RealtimeUpdate,timerKey);
    }
    public static void StopTimer(string timerKey)
    {
        Timing.KillCoroutines(timerKey);
    }
    public static IEnumerator<float> startTimer(System.DateTimeOffset dateTime, System.Action onUpdate, System.Action onStop)
    {
        var duration = (dateTime - System.DateTime.UtcNow);
        while (duration > System.TimeSpan.Zero && dateTime > System.DateTime.UtcNow)
        {
            onUpdate?.Invoke();
            yield return Timing.WaitForSeconds(1);
            duration -= System.TimeSpan.FromSeconds(1);
        }
        onStop?.Invoke();
    }

}
