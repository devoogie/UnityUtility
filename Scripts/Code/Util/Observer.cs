using System.Collections.Generic;

public abstract class EventSubject<T,TEventParam> where T : IEventObserver<TEventParam>
{
    static List<T> targetList = new List<T>();

    public void OnNotify(TEventParam eventType)
    {
        for (int i = targetList.Count - 1; i >= 0; i--)
        {
            T target = targetList[i];
            target.OnNotify(eventType);
        }
    }
    public void OnNotify()
    {
        for (int i = targetList.Count - 1; i >= 0; i--)
        {
            T target = targetList[i];
            target.OnNotify();
        }
    }
    public static void Add(T observer)
    {
        targetList.Add(observer);
    }
    public static void Remove(T observer)
    {
        targetList.Remove(observer);
    }

}
public interface IEventObserver<TEventParam>
{
    void OnNotify();
    void OnNotify(TEventParam eventParam);

}