using System.Collections.Generic;

public abstract class UISpawner<T> : PoolableMono where T:UI<T>
{
    public List<T> uiElements = new List<T>();
    public int Count => uiElements.Count;
    public T CreateUI()
    {
        T newUIElement = PoolManager.Spawn<T>();
        newUIElement.rectTransform.Identity(transform);
        uiElements.Add(newUIElement);
        return newUIElement;
    }
    public void Clear()
    {
        for (int i = uiElements.Count - 1; i >= 0; --i)
            uiElements[i]?.Despawn();
        FreeUIs();
    }
    public void FreeUIs()
    {
        uiElements.Clear();
    }
    public void PopUIs()
    {
        for (int i = 0; i < uiElements.Count; ++i)
            if (uiElements.IsValid(i))
                uiElements[i].rectTransform.PopDrop(i + 1);
    }
    public void UpdateInfos()
    {
        for (int i = 0; i < uiElements.Count; ++i)
            if (uiElements.IsValid(i))
                uiElements[i].UpdateInfo();
    }
   
}