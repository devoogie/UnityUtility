using System.Collections.Generic;

public abstract class EventSubject<T> where T : IEventObserver
{
    static List<T> targetList = new List<T>();

    public static void OnNotify(object eventType)
    {
        for (int i = targetList.Count - 1; i >= 0; i--)
        {
            T target = targetList[i];
            target.OnNotify(eventType);
        }
    }
    public static void Subscribe(T observer)
    {
        targetList.Add(observer);
    }
    public static void Remove(T observer)
    {
        targetList.Remove(observer);
    }

}
public interface IEventObserver
{
    void OnNotify(object eventParam);

}