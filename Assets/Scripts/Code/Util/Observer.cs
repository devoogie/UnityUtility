using System.Collections.Generic;

public class ObserveSubject<T,TEvent>
{
    List<T> targetList = new List<T>();

    public ObserveSubject<T,TEvent> OnNotify(TEvent eventType)
    {
        
        return this;
    }
}
public interface IObserver<T,TEvent>
{
    void OnNotify(TEvent TEvent);
    T AddObserver(ObserveSubject<T,TEvent> observer);
    T RemoveObserver(ObserveSubject<T,TEvent> observer);
}