using System.Collections.Generic;
public class UISpawner : UI
{
    public List<UI> elements = new List<UI>();
    public override void OnCreate()
    {
    }

    public override void OnSpawn()
    {
    }

    public override void OnHide()
    {
        HideAll();
    }


    public int Count => elements.Count;
    public T Show<T>() where T : UI
    {
        var spawned = PoolManager.Show<T>();
        elements.Add(spawned);
        spawned.transform.Identity(transform);
        return spawned;
    }
    public T Show<T>(string key) where T : UI
    {
        var spawned = PoolManager.Show<T>(key);
        elements.Add(spawned);
        spawned.rectTransform.Identity(transform);
        return spawned;
    }
    public void HideAll()
    {
        for (int i = elements.Count - 1; i >= 0; --i)
            PoolManager.Hide(elements[i]);
        elements.Clear();

    }
}