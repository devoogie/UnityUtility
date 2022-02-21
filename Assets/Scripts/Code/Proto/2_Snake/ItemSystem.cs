using UnityEngine;

public class ItemSystem
{
    public void CreateExp(Vector2 position)
    {
        var exp = PoolManager.Spawn<ItemExp>();
        exp.transform.position = position;
        int value = result.Random();
        exp.SetExp(value);
    }    
    public int[] result = new int[] {
        20,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
        1,
    };
}
